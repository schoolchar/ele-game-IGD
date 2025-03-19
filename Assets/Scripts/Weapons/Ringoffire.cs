using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ringoffire : MonoBehaviour
{
    public Transform player;
    public float distance = 1.0f;
    public float baseSpeed = 180.0f;
    private Vector3 lastPlayerPosition;
    private float playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        lastPlayerPosition = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerSpeed();
        FireRing();
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitEnemy(collision);
    }

    void HitEnemy(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit");
            Destroy(_collision.gameObject);
        }
    }

    void UpdatePlayerSpeed()
    {
        // Calculate the player's speed based on movement since the last frame
        Vector3 playerMovement = player.position - lastPlayerPosition;
        playerSpeed = playerMovement.magnitude / Time.deltaTime;
        lastPlayerPosition = player.position;
    }

    void FireRing()
    {
        // Add the player's speed to the base speed to get the adjusted speed
        float adjustedSpeed = baseSpeed + playerSpeed;

        // Rotate around the player with the adjusted speed
        transform.RotateAround(player.position, Vector3.up, adjustedSpeed * Time.deltaTime);

        // Maintain the specified distance from the player
        transform.position = player.position + (transform.position - player.position).normalized * distance;
    }

    // Method to set the distance dynamically
    public void SetDistance(float newDistance)
    {
        distance = newDistance;
    }

    // Method to set the base speed dynamically
    public void SetBaseSpeed(float newBaseSpeed)
    {
        baseSpeed = newBaseSpeed;
    }
}
