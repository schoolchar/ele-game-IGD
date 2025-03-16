using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercollisions : MonoBehaviour
{
    public bool Playerhasknife;
    public bool Playerhasfire;
    public bool firespawned;
    // Start is called before the first frame update
    void Start()
    {
        Playerhasknife = false;
        Playerhasfire = false;
        firespawned = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "fire")
        {
            Playerhasfire = true;
            firespawned = true;
            Debug.Log("Player has fire");
        }

        if(collision.gameObject.tag == "knife")
        {
            Playerhasknife = true;
            Debug.Log("Player has knife");
        }
    }
}
