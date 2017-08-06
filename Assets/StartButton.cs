using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public GameObject LoadingObject;
    public void StartGame()
    {
        LoadingObject.SetActive(true);
        SceneManager.LoadScene("Main");
    }
}