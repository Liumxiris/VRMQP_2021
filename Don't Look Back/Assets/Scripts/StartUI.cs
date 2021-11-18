using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    [SerializeField]
    GameObject StartMenu;

    [SerializeField]
    GameObject HelpMenu;

    [SerializeField]
    VideoPlayer HelperVideoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        HelpMenu.SetActive(false);
        Helper();
    }

    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    public void Helper() {
        StartMenu.SetActive(false);
        HelpMenu.SetActive(true);
        HelperVideoPlayer.Play();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ReturnToStart()
    {
        StartMenu.SetActive(true);
        HelpMenu.SetActive(false);
        HelperVideoPlayer.Stop();
    }
}
