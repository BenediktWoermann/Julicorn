using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && (collision.gameObject.name == "Ground" || collision.gameObject.name == "Ceiling" || System.Math.Abs(collision.gameObject.GetComponent<ObstacleMover>().speed) > Mathf.Epsilon))
        {
            GameObject.Find("GameManager").GetComponent<GameManagment>().endingGame = true;
            return;
        }
        if(collision.gameObject.tag == "Coin") {
            DataManagement.coins++;
            GameObject.Find("GameManager").GetComponent<GameManagment>().WriteStats();
            Destroy(collision.gameObject);
        }
    }
}
