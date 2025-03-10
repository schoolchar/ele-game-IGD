using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    // Start is called before the first frame update
   

    public void playGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void back2Menu()
    {
        SceneManager.LoadScene(0);
    }
 }
