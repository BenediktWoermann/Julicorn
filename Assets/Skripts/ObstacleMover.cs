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
    private float angle, anglevel, angleacc, gravity;
    private float startX;
    private float startY;
    private float lowerScreenBorder;

    //---------MOVEMENT----------
    public float speed;
    private Vector3 pos;
    // Start is called before the first frame of update
    void Start()
    {
        pos = GetComponent<Transform>().position;
        angle = 0;
        anglevel = 0;
        angleacc = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("GameManager").GetComponent<GameManagment>().endingGame)
        {
            //if (GameObject.Find("Score").GetComponent<ScoreUpdater>().counting)
            //{
                pos.x -= speed * Time.deltaTime;
            //}
            GetComponent<Transform>().position = pos;
        }

        // let tree fall frame for frame
        if (falling)
        {
            anglevel += angleacc * Time.deltaTime;
            angle += anglevel * Time.deltaTime;
            angleacc = Mathf.Sin(angle / 180 * Mathf.PI) * gravity;

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

    public void SetPos(Vector3 v) {
        pos = v;
        GetComponent<Transform>().position = pos;
    }

    public void SetYBorder(float border) {
        lowerScreenBorder = border;
    }

    public void TreeFall(float grav = 300f, float startingacc = 100f) {
        // Random fall time
        gravity = grav;
        angleacc = startingacc;
        falling = true;
        startX = pos.x;
        startY = pos.y;
        speed = 0;
    }
}
