using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBall : MonoBehaviour 
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SeaLion.targetHit = true;
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(2);
        }
    }
}
