using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    //---------FALLING----------
    // signals that in update, the tree should change angle to the ground in order to "fall"
    private bool falling;
    // gets set in TreeFall; is randomized
    private Quaternion rot;
    private float angle, anglevel, angleacc, gra;
    private float startX;
    private float startY;
    private float lowerScreenBorder;

    //---------MOVEMENT----------
    private Vector3 pos;
    public Vector2 speed;
    public float grav;
    // Start is called before the first frame of update
    void Start()
    {
        pos = gameObject.transform.position;
        angle = 0;
        anglevel = 0;
        angleacc = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        pos = gameObject.transform.position;
        Move();
        // let tree fall frame for frame
        if (falling)
        {
            anglevel += angleacc * Time.deltaTime;
            angle += anglevel * Time.deltaTime;
            angleacc = Mathf.Sin(angle / 180 * Mathf.PI) * gra;

            //-------ROTATION--------
            GetComponent<Transform>().RotateAround(pos, new Vector3(0, 0, 1), -anglevel * Time.deltaTime);
            if (angle >= 80) falling = false;

            //-------TRANSLATION--------
            Vector3 setPos = pos;
            float radius = startY - lowerScreenBorder;
            setPos.x = startX + Mathf.Sin(angle / 180 * Mathf.PI) * radius;
            setPos.y = startY - radius + Mathf.Cos(angle / 180 * Mathf.PI) * radius;
            pos = setPos;
            GetComponent<Transform>().position = pos;
        }
    }

    private void Move()
    {
        if (!GameObject.Find("GameManager").GetComponent<GameManagment>().endingGame || GameObject.Find("GameManager").GetComponent<GameManagment>().goldiAniStarted)
        {
            //horizontal
            pos.x -= speed.x * Time.deltaTime;
            GetComponent<Transform>().position = pos;

            //vertical
            speed.y += grav * Time.deltaTime;
            pos.y -= speed.y * Time.deltaTime;
            gameObject.transform.position = pos;
        }

    }

    public void SetPos(Vector3 v) {
        pos = v;
        GetComponent<Transform>().position = pos;
    }

    public void SetYBorder(float border) {
        lowerScreenBorder = border;
    }

    public void TreeFall(float gravity = 300f, float startingacc = 100f) {
        // Random fall time
        gra = gravity;
        angleacc = startingacc;
        falling = true;
        startX = pos.x;
        startY = pos.y;
        speed.x = 0;
        speed.y = 0;
    }
}
