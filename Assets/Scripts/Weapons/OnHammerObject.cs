using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHammerObject : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //damage stuff
            Debug.Log("Enemy hit");
        }
    }
}
