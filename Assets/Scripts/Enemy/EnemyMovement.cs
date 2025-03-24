using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public Transform player; // Player reference
    public int Movespeed;
    public bool playerAlive;
    void Start()
    {   
        playerAlive = true;
        Movespeed = 5;
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, Movespeed * Time.deltaTime);
        transform.LookAt(player);

        if (playerAlive == false)
        {
            player = null;
        }
    }
}