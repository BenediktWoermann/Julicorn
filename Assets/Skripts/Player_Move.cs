using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public bool jumpable, playerInitialized, gameStoped;
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
        if ((Input.touches.Length>0 || Input.GetKey(KeyCode.Space)) && GameObject.Find("Main Camera").transform.position.y < Mathf.Epsilon && jumpable && !gameStoped)
        {
            Jump();
            if(!playerInitialized && Math.Abs(GetComponent<Rigidbody2D>().gravityScale) < Mathf.Epsilon) {
                Init();
            }
            jumpable = false;
        }
        if(!jumpable && Input.touches.Length == 0 && !Input.GetKey(KeyCode.Space))
        {
            jumpable = true;
        }
    }

    public void Jump() {
        RB.velocity = Vector3.zero;
        RB.AddForce(jump * jumpForce, ForceMode2D.Impulse);
    }

    public void Setactive() {
        playerInitialized = false;
    }

    public void Init()
    {
        GameObject.Find("Score").GetComponent<ScoreUpdater>().counting = true;
        GetComponent<Rigidbody2D>().gravityScale = 3;
        GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>().active = true;
        playerInitialized = true;
        GameObject.Find("Foreground").GetComponent<BG_Move>().speed = 0.08f;
        GameObject.Find("Cloud").GetComponent<ObstacleMover>().speed = 3f;
        List<GameObject> toMove = GameObject.Find("ObstacleSpawner").GetComponent<ObstacleSpawner>().tubes;
        for(int i = toMove.Count; i>0; i--) {
            if (toMove[i - 1] != null)
            {
                toMove[i - 1].GetComponent<ObstacleMover>().speed = 2f;
            }
        }
    }


}
