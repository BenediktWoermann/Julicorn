using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;

    // keep track of all obstacles and coins in this list to destroy them later
    public List<GameObject> tubes = new List<GameObject>();
    public List<GameObject> coins = new List<GameObject>();


    // Height of the gate where you have to fly through in % of the height
    public float gateHeightMax, gateHeightMin;
    public int repetitionTimeMax, repetitionTimeMin;
    public int frames;
    private float width, height;
    public bool active;

    // Update is called once per frame
    void Start()
    {
        frames = 0;
        height = Camera.main.orthographicSize * 2f;
        width = height / Screen.height * Screen.width;
    }

    private void Update()
    {
        if (active)
        {
            // leave out first two cycles if there are fallen trees, so that they dont block the way
            if (frames % repetitionTimeMax == 0 && (frames > repetitionTimeMax || tubes.Count == 0 || (tubes.Count == 2 && frames == repetitionTimeMax)))
            {
                AddObstacle(0);
            }
            if ((frames - repetitionTimeMax / 2) % repetitionTimeMax == 0)
            {
                if (GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite.name.Equals("GoldDonkey_0"))
                {
                    AddObstacle(2);
                }
                else {
                    AddObstacle(1);
                }
            }
            frames++;
        }
    }


    // index 0 for a random tree; index 1 for a coin; index 2 for straw
    public void AddObstacle(int index) {
        if (index == 0)
        {
            // Spawn tree
            int treenumber = (int)Random.Range(1, 4 - Mathf.Epsilon);
            float temp = frames / 30000f;
            if (temp > 1) temp = 1;
            float gap = (gateHeightMin - gateHeightMax) * temp + gateHeightMax;
            // Spawn Obstacle
            float upperGateLimit = Random.Range(gap * 100, 10000) / 10000;
            GameObject go = Instantiate(obstacles[treenumber], transform.position, Quaternion.identity);
            float x = width * .6f;
            go.transform.position = new Vector3(x, height * upperGateLimit - 2f, 0);
            GameObject go2 = Instantiate(obstacles[0], transform.position, Quaternion.identity);
            go2.transform.position = new Vector3(x, height * upperGateLimit - gap / 100 * height - go2.GetComponent<SpriteRenderer>().bounds.size.y - 2f, 0);
            tubes.Add(go);
            tubes.Add(go2);

            // Test for TreeFall in ObstacleMover, to try it in game
            //go.GetComponent<ObstacleMover>().TreeFall();
            //go2.GetComponent<ObstacleMover>().TreeFall();

            return;
        }
        if (index == 1) {
            // Spawn Coin
            if (Random.value > .5) { 
                float ypos = Random.value * height - height/2;
                GameObject go = Instantiate(obstacles[4], transform.position, Quaternion.identity);
                go.transform.position = new Vector3(width * .6f, ypos - 2, 0);
                coins.Add(go);
                return;
            }
        }
        if (index == 2)
        {
            // Spawn Straw
            if(Random.value > .3) {
                float ypos = Random.value * height - height / 2;
                GameObject go = Instantiate(obstacles[5], transform.position, Quaternion.identity);
                go.transform.position = new Vector3(width * .6f, ypos, 0);
                coins.Add(go);
                return;
            }
        } 
    }
}
