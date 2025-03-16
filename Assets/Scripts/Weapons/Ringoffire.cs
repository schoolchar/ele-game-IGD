using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ringoffire : MonoBehaviour
{
    public Transform player;
    public float distance = 1.0f;
    public float speed = 180.0f;
   // public int movespeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        FireRing();
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
          //  Destroy(this.gameObject);
        }
    }

    void FireRing()
    {
        transform.RotateAround(player.position, Vector3.up, speed * Time.deltaTime);
        transform.position = player.position + (transform.position - player.position).normalized * distance;
       // transform.position = Vector3.MoveTowards(transform.position, player.position, movespeed * Time.deltaTime);
    } 
}
