using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    public bool counting;
    public static int frames;
    public static float score;
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        frames = 0;
        txt.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (counting && !GameObject.Find("GameManager").GetComponent<GameManagment>().endingGame)
        {
            frames++;
            score += Time.deltaTime*4;
            txt.text = ((int)score).ToString();
        }
    }

    public void Reset()
    {
        txt.text = "0";
        frames = 0;
        score = 0f;
    }

    public int GetScore() {
        int scr;
        int.TryParse(txt.text, out scr);
        return scr;
    }
}
