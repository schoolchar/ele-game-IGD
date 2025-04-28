using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantWaterAttack : MonoBehaviour
{
    public AudioSource waterSound;

    void OnEnable()
    {
        if(waterSound != null)
            waterSound.Play();
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Attack(other.gameObject.GetComponent<EnemyHealth>());
        }
    }


    public void Attack(EnemyHealth _enemy)
    {
        _enemy.TakeDamage(0.2f); //Placeholder value
    }

}
