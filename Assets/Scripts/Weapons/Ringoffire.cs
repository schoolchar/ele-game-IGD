using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Ringoffire : WeaponBase
{
    ChooseWeapons chooseWeapons;
    public Transform player;
    public int RingoffireLevel;
    public float distance = 1.0f;
    public float baseSpeed = 180.0f;
    float oldSpeed;
    float speedUpgrade = 0.25f;
    private Vector3 lastPlayerPosition;
    private float playerSpeed;
    float adjustedSpeed;
    bool fireActive;

    // Start is called before the first frame update
    void Start()
    {
       // RingoffireLevel = chooseWeapons.allWeaponsData[0].level; //Gets the level of ring of fire
    }
    public override void ActivateThisWeapon()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        lastPlayerPosition = player.position;
        fireActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Fire activated");
        if (fireActive)
        {
            UpdatePlayerSpeed();
            FireRing();
        }
        else
        {
            baseSpeed += (baseSpeed + playerSpeed) * 1.5f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitEnemy(collision);
    }

    void HitEnemy(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Enemy"))
        {
            _collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
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
        adjustedSpeed = (baseSpeed + playerSpeed) * 1.5f;

        //What I think might work for the ring of fire upgrade. Supposed to incease the speed of the ring of fire every time it is upgraded

      /*  if(RingoffireLevel == 1)
        {
            adjustedSpeed = (baseSpeed + playerSpeed) * 1.5f;
            oldSpeed = adjustedSpeed;
        }
        if(RingoffireLevel > 1)
        {
            adjustedSpeed = oldSpeed * speedUpgrade;
        }*/

        // Rotate around the player with the adjusted speed
        transform.RotateAround(player.position, Vector3.down, adjustedSpeed * Time.deltaTime);

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
