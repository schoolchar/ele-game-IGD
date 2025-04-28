using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnOffMusic : MonoBehaviour
{
    private bool isInGameScene;
    public AudioSource audioSource;
    private bool hasPlayed = false;

    // Update is called once per frame
    void Update()
    {
        //gets scene name
        Scene currentScene = SceneManager.GetActiveScene();
        isInGameScene = currentScene.name == "GameScene";

        //If player is in the game scene
        if (isInGameScene == true && hasPlayed == false)
        {
            if (audioSource != null)
            {
                audioSource.Play();
                hasPlayed = true;
            }
        }
    }
}
