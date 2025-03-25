using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool isPaused;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        playerHealth.InitValues();
        playerMovement = playerHealth.gameObject.GetComponent<PlayerMovement>();
        playerMovement.InitValues();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
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

    public void QuitPause()
    {
        SceneManager.LoadScene(0);
    }
}
