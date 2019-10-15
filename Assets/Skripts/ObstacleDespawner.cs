using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDespawner : MonoBehaviour
{
    private float width, height;
    //this bool shows if the object was already on screen at any time
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        height = Camera.main.orthographicSize * 2f;
        width = height / Screen.height * Screen.width;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        float x = GetComponent<Transform>().position.x;
        float y = GetComponent<Transform>().position.y;
        if ((x < -width || x > width || y < -height || y > height) && active) Destroy(gameObject);
        if (x > -width / 2 && x < width / 2 && y > -height / 2 && y < height / 2) active = true;
    }
}
