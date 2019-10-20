using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchasing : MonoBehaviour
{
    public static int[] prices;
    public List<Sprite> sprites;
    public List<Button> skinButtons;
    public Button buybtn;
    public Text coins, tooExpensive;
    private int selectedSkin;

    // Variables for alpha animation of "too expensive" text
    private bool alphaIncreasing, timing, alphaDecreasing;
    private float alphaTimer;

    private void Start()
    {
        buybtn.onClick.AddListener(() => { PurchaseSkin(selectedSkin);});
        prices = new int[]{0, 200, 1000};
    }

    private void Update()
    {
        // Duration of blending in
        if (alphaIncreasing) {
            if (tooExpensive.GetComponent<Text>().color.a >= 1) {
                alphaIncreasing = false;
                timing = true;
            } else {
                Color temp = tooExpensive.GetComponent<Text>().color;
                temp.a += 0.02f;
                tooExpensive.GetComponent<Text>().color = temp;
            }
        }
        // Duration of showing the message
        if (timing) {
            alphaTimer += Time.deltaTime;
            if (alphaTimer > .4f) {
                timing = false;
                alphaDecreasing = true;
            }
        }
        // Duration of blending out
        if (alphaDecreasing) {
            if (tooExpensive.GetComponent<Text>().color.a <= 0)
            {
                alphaDecreasing = false;
            }
            else
            {
                Color temp = tooExpensive.GetComponent<Text>().color;
                temp.a -= 0.02f;
                tooExpensive.GetComponent<Text>().color = temp;
            }
        }
    }

    private void PurchaseSkin(int skinNr) {
        if (DataManagement.purchasedSkins[skinNr])
        {
            // Select skin
            buybtn.transform.position = new Vector3(6000, 6000, 0);
            GameObject.Find("GameManager").GetComponent<GameManagment>().StopShop();
            GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = sprites[skinNr];
            GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX = (skinNr == 1);
        }
        else
        {
            // Try to buy skin
            if (DataManagement.coins >= prices[skinNr])
            {
                // Buy Skin
                DataManagement.coins -= prices[skinNr];
                coins.text = DataManagement.coins.ToString();
                DataManagement.purchasedSkins[skinNr] = true;
                buybtn.GetComponentInChildren<Text>().text = "select";
                Save_Load.SaveData();
                GameObject.Find("ShopSpawner").GetComponent<ShopSpawner>().EnableSkin(skinNr);
            }
            else
            {
                // Skin is too expensive; show the message
                Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
                Vector3 pos = new Vector3
                {
                    x = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height * 3 / 4, 0)).x,
                    y = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height * 3 / 4, 0)).y,
                    z = 0
                };
                tooExpensive.transform.position = pos;
                if (!alphaIncreasing && !timing && !alphaDecreasing)
                {
                    alphaIncreasing = true;
                }
            }
        }
    }

    public void SetListeners() {
        for (int i = 0; i < skinButtons.Count; i++)
        {
            int a = i;
            skinButtons[i].onClick.AddListener(() => { SkinClicked(a); });
        }
    }

    public void SkinClicked(int skinNr) {
        if (DataManagement.purchasedSkins[skinNr])
        {
            // write "select" on button
            buybtn.GetComponentInChildren<Text>().text = "select";
            selectedSkin = skinNr;
        }
        else
        {
            // write "purchasing" on button
            buybtn.GetComponentInChildren<Text>().text = "purchase";
            selectedSkin = skinNr;
        }
        // position the button
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 pos = new Vector3
        {
            x = Screen.width * (skinNr + 1) / 4,
            y = Screen.height / 4 + ShopSpawner.generalOffset,
            z = 0
        };
        buybtn.transform.position = Camera.main.ScreenToWorldPoint(pos);
        pos = buybtn.GetComponent<RectTransform>().localPosition;
        pos.z = 0;
        buybtn.GetComponent<RectTransform>().localPosition = pos;
    }
}
