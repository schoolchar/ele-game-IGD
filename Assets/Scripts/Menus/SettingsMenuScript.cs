using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuScript : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("hi");
    }
    
}
