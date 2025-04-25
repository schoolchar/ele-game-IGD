using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour
{
    public AudioSource fireSound;
    // Start is called before the first frame update
    void Start()
    {
        fireSound = GetComponent<AudioSource>();
        fireSound.Play(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            fireSound.Pause();
        }
        else
        {
            fireSound.UnPause();
        }
        
    }
}
