using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public Transform player; // Player reference
    public int Movespeed;
    void Start()
    {
        Movespeed = 5;
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        //player = player.transform;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, Movespeed * Time.deltaTime);
        transform.LookAt(player);
    }
}