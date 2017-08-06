using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public AudioClip OpenWindow;
    public AudioClip Accept;

    public GameObject LoadingObject;
    public AudioSource AudioSource;

    public void Start()
    {
        AudioSource.PlayOneShot(OpenWindow);
    }

    public void StartGame()
    {
        AudioSource.PlayOneShot(Accept);
        LoadingObject.SetActive(true);
        SceneManager.LoadScene("Main");
    }
}