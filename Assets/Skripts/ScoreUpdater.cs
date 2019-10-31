using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    public bool counting;
    public static int frames;
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
            txt.text = (frames / 10).ToString();
        }
    }

    public void Reset()
    {
        txt.text = "0";
        frames = 0;
    }

    public int GetScore() {
        int scr;
        int.TryParse(txt.text, out scr);
        return scr;
    }
}
