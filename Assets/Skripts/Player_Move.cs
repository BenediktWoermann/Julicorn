using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public bool jumpable, playerInitialized, gameStoped;
    public static bool goldiEvent, firstGoldiGame;
    public Rigidbody2D RB;
    public Vector3 jump;
    public float jumpForce = .2f;
    public int lastJump = -1;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        jump = new Vector3(0f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touches.Length > 0 || Input.GetKey(KeyCode.Space)) && GameObject.Find("Main Camera").transform.position.y < Mathf.Epsilon && jumpable && (!gameStoped || firstGoldiGame) && !goldiEvent)
        {
            if (!playerInitialized && Math.Abs(GetComponent<Rigidbody2D>().gravityScale) < Mathf.Epsilon) {
                Init(firstGoldiGame);
            }
            Jump();
            jumpable = false;
        }
        if (!jumpable && Input.touches.Length == 0 && !Input.GetKey(KeyCode.Space))
        {
            jumpable = true;
        }
        GameObject player = GameObject.Find("Player");
        GameObject dh = GameObject.Find("doghouse");
        if (dh != null)
        {
            if (!gameStoped && player.transform.position.x - dh.transform.position.x > 3.7f && Mathf.Abs(player.GetComponent<Rigidbody2D>().gravityScale) < Mathf.Epsilon)
            {
                player.GetComponent<Rigidbody2D>().gravityScale = 3f;
                Jump();
                player.GetComponent<CapsuleCollider2D>().isTrigger = true;
            }
        }
    }

    public void Jump() {
        if (Mathf.Abs(GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale) < Mathf.Epsilon) {
            return;
        }
        RB.velocity = Vector3.zero;
        RB.AddForce(jump * jumpForce, ForceMode2D.Impulse);
    }

    public void Setactive() {
        playerInitialized = false;
    }

    public void Init(bool moveDoghouse = false)
    {
        GameObject.Find("Score").GetComponent<ScoreUpdater>().counting = true;
        GetComponent<Rigidbody2D>().gravityScale = 3;
        GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>().active = true;
        playerInitialized = true;
        GameObject.Find("Foreground").GetComponent<BG_Move>().speed = 0.08f;
        print(GameObject.Find("Foreground").GetComponent<BG_Move>().speed);
        GameObject.Find("Cloud").GetComponent<ObstacleMover>().speed.x = 3f;
        List<GameObject> toMove = GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>().tubes;
        for(int i = toMove.Count; i>0; i--) {
            if (toMove[i - 1] != null)
            {
                toMove[i - 1].GetComponent<ObstacleMover>().speed.x = 2f;
            }
        }

        if (moveDoghouse)
        {
            gameStoped = false;
            firstGoldiGame = false;
            GameObject.Find("doghouse").GetComponent<ObstacleMover>().speed.x = 2f;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }


}
