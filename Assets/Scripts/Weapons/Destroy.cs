using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 8f);  
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //If a weapon collides with an enemy
        HitEnemy(collision);
    }

    /// <summary>
    /// For assigned weapons, destroys the instance of the weapon when colliding with the enemy
    /// </summary>
    void HitEnemy(Collision _collision)
    {
        //If the knife collides with an enemy, enemy takes damage
        if(_collision.gameObject.layer == 8)
        {
            //Debug.Log("Enemy hit");
            //Do damage to enemy, then destroy this
            _collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            Destroy(this.gameObject);
        }
    } //END HitEnemy()
}
