using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotater : MonoBehaviour
{
    private float rotation;
    private float maxWidth;
    // Start is called before the first frame update
    void Start()
    {
        rotation = 0;
        maxWidth = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        rotation += Time.deltaTime;
        Vector2 scale = transform.localScale;
        scale.x = maxWidth * Mathf.Abs(Mathf.Cos(rotation));
        transform.localScale = scale;
    }
}
