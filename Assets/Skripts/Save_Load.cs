using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Save_Load
{
    public static void SaveData()
    {
        //Debug.Log("Before Save:    Coins: " + DataManagement.coins + "     Highscore: " + DataManagement.highscore);
        //Debug.Log("Saving Data!");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataType data = new DataType();
        formatter.Serialize(stream, data);
        stream.Close();
        //Debug.Log("After Save:    Coins: " + DataManagement.coins + "     Highscore: " + DataManagement.highscore);
    }

    public static DataType LoadData()
    {
        string path = Application.persistentDataPath + "/data.txt";
        if (File.Exists(path))
        {
            //Debug.Log("Before Load:    Coins: " + DataManagement.coins + "     Highscore: " + DataManagement.highscore);
            //Debug.Log("Loading Data!");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DataType data = formatter.Deserialize(stream) as DataType;
            stream.Close();
            // purchased array should have the length of available skins;
            // if its shorter due to an update of skins -> copy to longer array
            if(data.purchasedSkins == null || data.purchasedSkins.Length < Info.skinCount) {
                bool[] purch = new bool[Info.skinCount];
                for(int i = 0; i<purch.Length; i++) {
                    purch[i] = false;
                }
                if (data.purchasedSkins == null)
                {
                    data.purchasedSkins = purch;
                }
                else
                {
                    data.purchasedSkins.CopyTo(purch, 0);
                    data.purchasedSkins = purch;
                }
            }
            //starter skin is always purchased
            data.purchasedSkins[0] = true;

            // GOD MODE
            data.purchasedSkins[1] = false;
            data.purchasedSkins[2] = false;
            data.purchasedSkins[3] = true;
            data.coins = 9999;

            //Debug.Log("After Load:    Coins: " + DataManagement.coins + "     Highscore: " + DataManagement.highscore);
            return data;
        }
        else {
            Debug.LogError("File " + path + " not found!");
            return null;
        }
    }
}

[System.Serializable]
public class DataType{
    public int highscore, coins;
    public bool[] purchasedSkins;
    public DataType()
    {
        highscore = DataManagement.highscore;
        coins = DataManagement.coins;
        purchasedSkins = DataManagement.purchasedSkins;
    }
}
