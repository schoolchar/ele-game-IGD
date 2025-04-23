using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBall : MonoBehaviour 
{
    public AudioSource ballSound;

    // Start is called before the first frame update
    void Start()
    {
        ballSound = GetComponent<AudioSource>();
        ballSound.Play();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SeaLion.targetHit = true;
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }
}
