using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BG_Scaler : MonoBehaviour
{
    // Start is called before the first frame update
    public float scale;
    private Material mat;
    private Texture tex;
    private Vector2 widhei;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        tex = mat.GetTexture("_MainTex");
        widhei = new Vector2(tex.width, tex.height);
        float height = Camera.main.orthographicSize * 2f;
        float width = height / Screen.height * Screen.width;
        //height = width / widhei.x * widhei.y;
        transform.localScale = new Vector3(scale*width, scale*height, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
