using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSpawner : MonoBehaviour
{
    public Purchasing purch;

    public GameObject arrowPrefab;

    public GameObject txtPrefab;

    public static float cloudOffset, priceOffset, generalOffset;
    protected bool isHidden;

    public GameObject[] skinsPre;
    public GameObject[] darkSkinsPre;
    public GameObject cloudPre;

    // keep track of all Clouds and Skins in this list to destroy them later
    public List<GameObject> clouds = new List<GameObject>();
    public List<GameObject> skins = new List<GameObject>();

    private float width, height;

    // Start is called before the first frame update
    void Start()
    {
        cloudOffset = -.6f;
        priceOffset = 0f;
        generalOffset = -.3f;
        height = Camera.main.orthographicSize * 2f;
        print(height);
        width = height / Screen.height * Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        // set skins clouds and pricetags to an independent position to the canvas
        RectTransform canpos = GameObject.Find("Canvas").GetComponent<RectTransform>();
        // get height of first skin
        float skinHeight = 0;
        if (skins.Count > 0)
        {
            skinHeight = skins[0].GetComponent<Renderer>().bounds.extents.y * 2;
        }
        for (int i = 0; i<skins.Count; i++) {
            Vector3 pos = skins[i].transform.position;
            pos.y = generalOffset + height*2;
            skins[i].transform.position = pos;

            pos = clouds[i].transform.position;
            pos.y = generalOffset + cloudOffset * skinHeight + height*2;
            clouds[i].transform.position = pos;
        }
    }

    public void EnableSkin(int skinNr){
        // destroy grey variant
        GameObject toDestroy = skins[skinNr];
        skins.RemoveAt(skinNr);
        Destroy(toDestroy);

        // spawn coloured variant
        Vector3 pos = new Vector3
        {
            y = Screen.height * 2 + generalOffset,
            x = Screen.width * (skinNr + 1) / (skinsPre.Length + 1)
        };
        pos = Camera.main.ScreenToWorldPoint(pos);
        pos.z = 0;
        GameObject skin;
        skin = Instantiate(skinsPre[skinNr], pos, Quaternion.identity, GameObject.Find("Canvas").transform);
        skins.Insert(skinNr,skin);

        CanvasScaler can = GameObject.Find("Canvas").GetComponent<CanvasScaler>();
        skin.transform.localScale = can.referencePixelsPerUnit * skin.transform.localScale;

        // Set listener for new sprite
        for(int i = 0; i<skins.Count; i++) {
            purch.skinButtons[i] = skins[i].GetComponent<Button>();
        }
        purch.SetListeners();

        // Change pricetag
        float skinHeight = 0;
        if (skins.Count > 0)
        {
            skinHeight = skins[0].GetComponent<Renderer>().bounds.extents.y * 2;
        }
        pos.y += skinHeight * priceOffset;
        GameObject pricetag = Instantiate(txtPrefab, pos, Quaternion.identity, GameObject.Find("Canvas").transform);
        pricetag.transform.localScale = can.referencePixelsPerUnit * pricetag.transform.localScale /100;
        pricetag.GetComponent<Text>().text = "purchased!";
        Destroy(toDestroy);
    }

    public void Spawn() {
        // Set check variables to false and move player and cloud out of the scene
        GameObject.Find("Cloud").GetComponent<ObstacleMover>().SetPos(new Vector3(-6000, -6000, 0));
        GameObject.Find("Score").GetComponent<ScoreUpdater>().Reset();
        GameObject.Find("Player").transform.position = new Vector3(-6000, -6000, 0);
        CanvasScaler can = GameObject.Find("Canvas").GetComponent<CanvasScaler>();

        for (int i = 0; i<skinsPre.Length; i++) {
            if (i != 3 || DataManagement.purchasedSkins[3])
            {
                // Spawn every skin with own cloud in canvas
                Vector3 pos = new Vector3
                {
                    y = Screen.height * 2 + generalOffset,
                    x = Screen.width * (i + 1) / (skinsPre.Length + 1)
                };
                pos = Camera.main.ScreenToWorldPoint(pos);
                pos.z = 0;
                GameObject skin;
                if (DataManagement.purchasedSkins[i])
                {
                    skin = Instantiate(skinsPre[i], pos, Quaternion.identity, GameObject.Find("Canvas").transform);
                }
                else
                {
                    skin = Instantiate(darkSkinsPre[i], pos, Quaternion.identity, GameObject.Find("Canvas").transform);
                }
                skins.Add(skin);
                skin.transform.localScale = can.referencePixelsPerUnit * skin.transform.localScale;
                float skinHeight = 0;
                if (skins.Count > 0)
                {
                    skinHeight = skins[0].GetComponent<Renderer>().bounds.extents.y * 2;
                }
                //spawn clouds
                pos.y += skinHeight * cloudOffset;
                GameObject cloud = Instantiate(cloudPre, pos, Quaternion.identity, GameObject.Find("Canvas").transform);
                cloud.transform.localScale = can.referencePixelsPerUnit * cloud.transform.localScale;
                clouds.Add(cloud);
            }
        }
        for(int i = 0; i<skins.Count; i++) {
            purch.skinButtons[i] = skins[i].GetComponent<Button>();
        }
        purch.SetListeners();
        isHidden = false;
        Debug.Log("Shop Spawned!");
    }

    public void Despawn() {
        for (int i = skins.Count; i > 0; i--)
        {
            if(skins[i-1] != null) {
                Destroy(skins[i - 1]);
            }
        }
        for (int i = clouds.Count; i > 0; i--) {
            if(clouds[i-1] != null) {
                Destroy(clouds[i - 1]);
            }
        }
        skins = new List<GameObject>();
        clouds = new List<GameObject>();

        isHidden = true;
    }
}
