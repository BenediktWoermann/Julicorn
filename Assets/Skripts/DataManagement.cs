using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//[System.Serializable]
public class DataManagement
{
    public static int coins;
    public static int highscore;
    public static bool[] purchasedSkins = new bool[Info.skinCount];

    //public DataManagement() {
        //coins = GameObject.Find("GameManager").GetComponent<GameManagment>().coins;
        //highscore = GameObject.Find("GameManager").GetComponent<GameManagment>().highscore;
    //}
}
