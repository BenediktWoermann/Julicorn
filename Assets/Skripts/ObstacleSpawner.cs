using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    // keep track of all obstacles and coins in this list to destroy them later
    public List<GameObject> tubes = new List<GameObject>();
    public List<GameObject> coins = new List<GameObject>();


    // Height of the gate where you have to fly through in % of the height
    public float gateHeightMax, gateHeightMin;
    public int repetitionTimeMax, repetitionTimeMin;
    public int frames;
    private float width, height;
    public bool active;

    // GoldiEvent variables
    public static bool eventDone;
    public Sprite goldiSprite;

    // Update is called once per frame
    void Start()
    {
        frames = 0;
        height = Camera.main.orthographicSize * 2f;
        width = height / Screen.height * Screen.width;
        eventDone = DataManagement.purchasedSkins[3];
    }

    private void Update()
    {

        if (active)
        {
            if (ScoreUpdater.frames > 5000 && !eventDone) {
                // start GoldiEvent
                Player_Move.goldiEvent = true;
            }
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
            GameObject go = Instantiate(obstaclePrefabs[treenumber], transform.position, Quaternion.identity);
            float x = width * .6f;
            go.transform.position = new Vector3(x, height * upperGateLimit - 2f, 0);
            GameObject go2 = Instantiate(obstaclePrefabs[0], transform.position, Quaternion.identity);
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
                GameObject go = Instantiate(obstaclePrefabs[4], transform.position, Quaternion.identity);
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
                GameObject go = Instantiate(obstaclePrefabs[5], transform.position, Quaternion.identity);
                go.transform.position = new Vector3(width * .6f, ypos, 0);
                coins.Add(go);
                return;
            }
        } 
    }

    public void GoldiEvent() {
        GameObject.Find("GameManager").GetComponent<GameManagment>().endingGame = true;
        GameObject.Find("GameManager").GetComponent<GameManagment>().goldiEvent = true;
        print("GoldiEvent() started event!");
    }

    public void StartGoldiAnimation() {
        // first frame; set animations of background
        GameObject.Find("Player").GetComponent<CapsuleCollider2D>().isTrigger = false;
        GameObject.Find("Foreground").GetComponent<BG_Move>().speed = 0;
        GameObject.Find("Background").GetComponent<BG_Move>().speed = 0;
    }

    public bool StepGoldiAnimation() {
        GameObject player = GameObject.Find("Player");
        GameObject doghouse = GameObject.Find("doghouse");
        BG_Move fgmove = GameObject.Find("Foreground").GetComponent<BG_Move>();
        BG_Move bgmove = GameObject.Find("Background").GetComponent<BG_Move>();
        fgmove.speed = Mathf.Min(Mathf.Abs(player.transform.position.x + 6.7f), Mathf.Abs(player.transform.position.x + 150f))/36 + 0.02f;
        bgmove.speed = Mathf.Min(Mathf.Abs(player.transform.position.x + 6.7f), Mathf.Abs(player.transform.position.x + 150f))/36 + 0.02f;
        for (int i = 0; i<tubes.Count; i++) {
            if (tubes[i] != null)
            {
                tubes[i].GetComponent<ObstacleMover>().speed.x = 25 * fgmove.speed;
            }
        }
        for (int i = 0; i < coins.Count; i++)
        {
            if (coins[i] != null)
            {
                coins[i].GetComponent<ObstacleMover>().speed.x = 25 * fgmove.speed;
            }
        }
        GameObject.Find("Player").GetComponent<ObstacleMover>().speed.x = 25 * fgmove.speed;
        doghouse.GetComponent<ObstacleMover>().speed.x = 25 * fgmove.speed;
        if (player.transform.position.x <= -150f) {
            // End of animation; return true; set all speeds to zero
            fgmove.speed = 0;
            bgmove.speed = 0;
            for (int i = 0; i < tubes.Count; i++)
            {
                if(tubes[i] != null) tubes[i].GetComponent<ObstacleMover>().speed.x = 0;
            }
            for (int i = 0; i < coins.Count; i++)
            {
                if(coins[i] != null) coins[i].GetComponent<ObstacleMover>().speed.x = 0;
            }
            player.GetComponent<ObstacleMover>().speed.x = 0;
            doghouse.GetComponent<ObstacleMover>().speed.x = 0;
            GameObject.Find("Player").GetComponent<Player_Move>().Setactive();
            GameObject.Find("Player").GetComponent<Player_Move>().gameStoped = false;
            GameObject.Find("Player").transform.position = new Vector3(-6.7f, -1.96f, 0);
            player.GetComponent<SpriteRenderer>().sprite = goldiSprite;
            Player_Move.goldiEvent = false;
            eventDone = true;
            DataManagement.purchasedSkins[3] = true;
            Save_Load.SaveData();
            return true;
        }
        return false;
    }
}
