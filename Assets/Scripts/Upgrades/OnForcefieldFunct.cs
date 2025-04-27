using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnForcefieldFunct : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Destroy(collision.gameObject);
        }
    }
}
