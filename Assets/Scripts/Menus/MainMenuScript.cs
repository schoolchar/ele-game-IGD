using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Settings()
    {
        SceneManager.LoadScene(3);
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene(2);
    }
    public void Store()
    {
        SceneManager.LoadScene(4);
    }
    public void exit()
    {
        Application.Quit();
    }
}
