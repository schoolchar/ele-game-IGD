using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealBall : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyAfterWait());
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if this collides with an enemy, takes health from enemy
        if(collision.gameObject.layer == 8)
        {
            EnemyHealth _enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            _enemyHealth.TakeDamage(3);
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
