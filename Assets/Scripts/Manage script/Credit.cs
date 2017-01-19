using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
