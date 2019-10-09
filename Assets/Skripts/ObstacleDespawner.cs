using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDespawner : MonoBehaviour
{
    private float width, height;
    // Start is called before the first frame update
    void Start()
    {
        height = Camera.main.orthographicSize * 2f;
        width = height / Screen.height * Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Transform>().position.x < -width)
        {
            Destroy(gameObject);
        }
    }
}
