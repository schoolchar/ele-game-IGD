using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject storeButton;

    private void Start()
    {
       
        //Check if player has played at least once, if so enable button for store
       /* if(FindAnyObjectByType<PlayerMovement>() != null || FindAnyObjectByType<SaveData>().reset == false)
        {
            storeButton.SetActive(true);
        }
        else
        {
            storeButton.SetActive(false);
        }*/
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

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
