using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameObject droppingCoinPrefab;

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
        if (!Player_Move.goldiEvent && collision.gameObject.tag == "Obstacle" && (collision.gameObject.name == "Ground" || collision.gameObject.name == "Ceiling" || System.Math.Abs(collision.gameObject.GetComponent<ObstacleMover>().speed.x) > Mathf.Epsilon))
        {
            GameObject.Find("GameManager").GetComponent<GameManagment>().endingGame = true;
            return;
        }
        if(collision.gameObject.tag == "Coin") {
            DataManagement.coins++;
            GameObject.Find("GameManager").GetComponent<GameManagment>().WriteStats();
            Destroy(collision.gameObject);
            if(GameObject.Find("Player").GetComponent<SpriteRenderer>().flipX) {
                DropCoin();
            }
        }
        if (Player_Move.goldiEvent && !ObstacleSpawner.eventDone) {
            GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>().GoldiEvent();
        }
    }

    private void DropCoin() {
        Vector3 pos = droppingCoinPrefab.transform.position;
        pos.y = GameObject.Find("Player").transform.position.y;
        Instantiate(droppingCoinPrefab,pos,Quaternion.identity);   
    }
}
