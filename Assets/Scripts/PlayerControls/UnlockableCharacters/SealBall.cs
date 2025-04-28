using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SealBall : MonoBehaviour
{
    public AudioSource sealBallBounce;
    private bool isInGameScene;

    private void Start()
    {
        StartCoroutine(DestroyAfterWait());
    }

    void Update()
    {
        //gets scene name
        Scene currentScene = SceneManager.GetActiveScene();
        isInGameScene = currentScene.name == "GameScene";


        //If player is in the game scene, enemies can spawn, else they cannot
        if (isInGameScene == true)
         {
             sealBallBounce.UnPause();
         }
         else
         {
            sealBallBounce.Pause();
         }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if this collides with an enemy, takes health from enemy
        if(collision.gameObject.layer == 8)
        {
            EnemyHealth _enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            _enemyHealth.TakeDamage(3);
            sealBallBounce.UnPause();
            //Destroy(this.gameObject);
        }
       
    } //END OnCollisionEnter()

    /// <summary>
    /// Destroys the ball after 5 seconds
    /// </summary>
    IEnumerator DestroyAfterWait()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    } //END DestroyAfterWait()
}
