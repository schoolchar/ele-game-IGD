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
        //DO damage every frame the enemy is contacting the collider of the water
        if (other.gameObject.tag == "Enemy")
        {
            Attack(other.gameObject.GetComponent<EnemyHealth>());
        }
    }

    /// <summary>
    /// Do damage
    /// </summary>
    public void Attack(EnemyHealth _enemy)
    {
        if (_enemy != null)
            _enemy.TakeDamage(0.2f); 
    } //END Attack()

}
