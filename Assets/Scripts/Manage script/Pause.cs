using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    GameObject PauseUI;

    private bool Paused = false;
	// Use this for initialization
	void Start ()
    {
        PauseUI.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
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

    public void Reprendre()
    {
        Paused = false;
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene("Menu");
    }
}
