using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausedupdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Time.timeScale = 1;
        }
    }
}
