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
        if(_collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            _collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            Destroy(this.gameObject);
        }
    }
}
