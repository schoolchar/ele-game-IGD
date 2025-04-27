using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusic : MonoBehaviour
{
    public static MainMenuMusic instance;
    public AudioSource music;
    public bool inGame;
    void Awake()
    {
        inGame = false;

        //Game object can be moved across scenes without being destroyed
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //Destroys the game object
        {
            Destroy(gameObject);
        }

        gameObject.GetComponent<AudioSource>().enabled = true;

        gameObject.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            inGame = true;
            gameObject.GetComponent<AudioSource>().Stop();
        }
        if(SceneManager.GetActiveScene().name == "MainMenu" && inGame == true)
        {
            gameObject.GetComponent<AudioSource>().Play();
            inGame = false;
        }
    }
}
