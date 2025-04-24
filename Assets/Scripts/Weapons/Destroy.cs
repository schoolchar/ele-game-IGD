using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 8f);  
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitEnemy(collision);
    }

    void HitEnemy(Collision _collision)
    {
        //If the knife collides with an enemy, enemy takes damage
        if(_collision.gameObject.layer == 8)
        {
            Debug.Log("Enemy hit");
            _collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            Destroy(this.gameObject);
        }
    }
}
