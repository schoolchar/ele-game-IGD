using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knifethrow : MonoBehaviour
{
    public int Knifespeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KnifeLifetime());  
    }

    // Update is called once per frame
    public void Update()
    {
        transform.Translate(Vector3.forward * Knifespeed * Time.deltaTime);
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
            Destroy(_collision.gameObject);
            Destroy(this.gameObject);
        }
    }
    IEnumerator KnifeLifetime()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
