using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusic : MonoBehaviour
{
    public static MainMenuMusic instance;
    public AudioSource music;
    void Awake()
    {
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
            gameObject.GetComponent<AudioSource>().Stop();
        }
        if(gameObject.GetComponent<AudioSource>().enabled == false)
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
