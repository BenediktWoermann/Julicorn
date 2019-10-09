using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    public int nextScene;
    public Button[] Buttons = new Button[4];


    void Start()
    {
        for(int i = 0; i<Buttons.Length; i++) { 
            if(Buttons[i] != null)
            {
                int temp = i;
                Buttons[i].onClick.AddListener(() => FadeToScene(temp));
            }
        }
    }

public void FadeToScene(int index) {
        animator.SetTrigger("FadeOut");
        nextScene = index;
}

public void LoadLevel() {
        SceneManager.LoadScene(nextScene);
}

}
