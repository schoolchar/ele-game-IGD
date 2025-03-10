using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
   // public Button UnPause;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        PauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Paused");
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UnPauseGame();
            Debug.Log("UnPaused");
        }

       // UnPause.onClick.AddListener(UnPauseGame);
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    public void UnPauseGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }

   /* private void TogglePause()
    {
        if (isPaused)
        {
            UnPauseGame();
        }
        else
        {
            PauseGame();
        }
    }*/
}
