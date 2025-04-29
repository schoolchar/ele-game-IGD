using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForcefieldSound : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();  
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            audioSource.Play();
        }
    }
}
