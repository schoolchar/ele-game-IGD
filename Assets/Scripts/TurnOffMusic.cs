using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnOffMusic : MonoBehaviour
{
    private bool isInGameScene;
    public AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        //gets scene name
        Scene currentScene = SceneManager.GetActiveScene();
        isInGameScene = currentScene.name == "GameScene";


        //If player is in the game scene, enemies can spawn, else they cannot
         if (isInGameScene == true)
         {
             audioSource.UnPause();
         }
         else
         { 
             audioSource.Pause();
         }

    }
}
