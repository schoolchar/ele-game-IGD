using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBall : MonoBehaviour
{
    public float enemyTakeDamage = 2;

    private GameObject enemy;
    EnemyHealth enemyHealth;

    public bool debugHealth;
    public AudioSource monkeyAttackSound;

    void Start()
    {
       
    }
    private void OnTriggerEnter(Collider collision)
    {
        HitEnemy(collision);
    }

   /// <summary>
   /// When monkey ball contacts an enemy, do damage
   /// </summary>
    void HitEnemy(Collider _collision)
    {
        if (_collision.gameObject.tag == "Enemy")
        { 
            monkeyAttackSound.Play();
            enemyHealth = _collision.gameObject.GetComponent<EnemyHealth>();
            if (debugHealth)
            {
                //change to percent max health
                enemyTakeDamage = 100;
            }

            
            enemyHealth.TakeDamage(enemyTakeDamage);
        }
    } //END HitEnemy()
}
