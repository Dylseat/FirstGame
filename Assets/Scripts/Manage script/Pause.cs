using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    GameObject PauseUI;

    private bool Paused = false;

    void Start()
    {
        PauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Paused = !Paused;
        }

        if (Paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        if (!Paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }

    }

    public void Resume()
    {
        Paused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        Paused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}