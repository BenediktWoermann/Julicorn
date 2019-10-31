using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadAtStart : MonoBehaviour
{
    public Text highscore;
    public Text coins;

    // Start is called before the first frame update
    void Start()
    {
        DataType read = Save_Load.LoadData();
        //starter skin is always purchased
        read.purchasedSkins[0] = true;
        DataManagement.purchasedSkins = read.purchasedSkins;
        DataManagement.coins = 0;
        DataManagement.highscore = 0;
        DataManagement.coins = read.coins;
        DataManagement.highscore = read.highscore;
        highscore.text = DataManagement.highscore.ToString();
        coins.text = DataManagement.coins.ToString();
        ObstacleSpawner.eventDone = DataManagement.purchasedSkins[3];
    }


    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        Debug.Log("Coins: "+DataManagement.coins+ "     Highscore: "+DataManagement.highscore);
    //    }
    //}

}
