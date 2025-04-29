using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElephantWaterAttack : MonoBehaviour
{
    public AudioSource waterSound;
    private bool isInGameScene;

    void OnEnable()
    {
        if (waterSound != null)
        {
            if (isInGameScene == true)
            {
               waterSound.Play();
            }
        }
    }

    void Update()
    {
        //gets scene name
        Scene currentScene = SceneManager.GetActiveScene();
        isInGameScene = currentScene.name == "GameScene";
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
        if (_enemy != null)
            _enemy.TakeDamage(0.2f); //Placeholder value
    }

}
