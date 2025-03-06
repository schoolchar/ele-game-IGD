using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;


    private void OnTriggerEnter(Collider other)
    {
        LoseHealth(other);
    }


    void LoseHealth(Collider _other)
    {
        if(_other.gameObject.tag == "Enemy")
        {
            health--;
        }
    }
}
