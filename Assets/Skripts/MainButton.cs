using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButton : MonoBehaviour
{
    public Button b;
    public Player_Move PM;
    public GameObject GO;
    // Start is called before the first frame update
    void Start()
    {
        b = GameObject.Find("MainButton").GetComponent<Button>();
        GO = GameObject.Find("GoldDonkey");
        PM = GO.GetComponent<Player_Move>();
        b.onClick.AddListener(PM.Jump);
        b.onClick.AddListener(Test);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Test() {
        Debug.Log("TEST");
    }

}
