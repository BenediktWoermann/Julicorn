using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkinSpawner : MonoBehaviour
{ 
    //public string[] names = {"Flappy", "Unicorn", "Reverse Golddonkey"};
    //public GameObject spawnee;
    //public Transform pos;


    //void Start()
    //{

    //    float height = Camera.main.orthographicSize * 2f;
    //    float width = height / Screen.height * Screen.width;
    //    for (int i = 0; i < names.Length; i++)
    //        {
                
    //            GameObject go = Instantiate(spawnee, pos.position, pos.rotation, GameObject.FindGameObjectWithTag("Canvas").transform);
    //            go.GetComponent<RectTransform>().position = new Vector3((i%3-1f)*width*1/3, (0.4f+0.15f*(int) (i/3))*height ,  0);
    //            go.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/3, Screen.height/8);
    //            go.GetComponentInChildren<Text>().text = names[i];
    //            int temp = i;
    //            go.GetComponent<Button>().onClick.AddListener(() => GameObject.Find("Fader").GetComponent<SceneChanger>().FadeToScene(temp));
    //        }
    //}
}
