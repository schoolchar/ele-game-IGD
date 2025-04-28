using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireSound : MonoBehaviour
{
    public AudioSource fireSound;
    private bool isInGameScene;

    // Start is called before the first frame update
    void Start()
    {
        fireSound = GetComponent<AudioSource>();
        fireSound.Play(); 
    }

    // Update is called once per frame
    void Update()
    {
        //gets scene name
        Scene currentScene = SceneManager.GetActiveScene();
        isInGameScene = currentScene.name == "GameScene";

        if (Time.timeScale == 0)
        {
            fireSound.Play();
        }
        
    }
}
