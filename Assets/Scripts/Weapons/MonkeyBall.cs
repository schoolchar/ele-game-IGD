using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBall : MonoBehaviour
{
    public float enemyTakeDamage = 10;

    private GameObject enemy;
    EnemyHealth enemyHealth;

    public bool debugHealth;
    public AudioSource monkeySound;

    void Start()
    {
        monkeySound = GetComponent<AudioSource>();
        monkeySound.Play();
    }
    private void OnTriggerEnter(Collider collision)
    {
        HitEnemy(collision);
    }

   
    void HitEnemy(Collider _collision)
    {
        if (_collision.gameObject.tag == "Enemy")
        {
            enemyHealth = _collision.gameObject.GetComponent<EnemyHealth>();
            if (debugHealth)
            {
                //change to percent max health
                enemyTakeDamage = 100;
            }

            Debug.Log("MONKEY BALL HITS ENEMY");
            enemyHealth.TakeDamage(enemyTakeDamage);
        }
    }
}
