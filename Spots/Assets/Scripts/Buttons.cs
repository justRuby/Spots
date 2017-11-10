using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    private void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
            case "TestFinishButton":
                GameObject.Find("ExChange").GetComponent<Text>().text = "1";
                break;

            case "RestartButton":
                GameObject.Find("ExChange").GetComponent<Text>().text = "2";
                break;

            case "NewButton":
                SceneManager.LoadScene("Game");
                break;

            case "ExitButton":
                SceneManager.LoadScene("Menu");
                break;

            case "PlayButton":
                SceneManager.LoadScene("Game");
                break;

            default:
                break;
        }
    }
}
