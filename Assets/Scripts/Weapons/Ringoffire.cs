using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Ringoffire : WeaponBase
{
    [SerializeField] ChooseWeapons chooseWeapons;
    public Transform player;
    private Quaternion playerRotation;
    public int RingoffireLevel;
    public float distance = 1.0f;
    public float baseSpeed = 180.0f;
    float oldSpeed = 180.0f;
    float speedUpgrade = 1.25f;
    private Vector3 lastPlayerPosition;
    private float playerSpeed;
    float adjustedSpeed;
    public bool fireActive;
    private int oldRingoffireLevel;

    // Start is called before the first frame update
    void Start()
    {
        chooseWeapons = FindAnyObjectByType<ChooseWeapons>();
        RingoffireLevel = chooseWeapons.allWeaponsData[0].level; //Gets the level of ring of fire
        oldRingoffireLevel = RingoffireLevel;
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
        RingoffireLevel = chooseWeapons.allWeaponsData[0].level; //Updates the level of the ring of fire
        
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
            Debug.Log("Ring of fire has hit enemy");
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

        if(oldRingoffireLevel != RingoffireLevel)
        {
            oldRingoffireLevel++;
            adjustedSpeed = oldSpeed * speedUpgrade;
        }

        oldSpeed = adjustedSpeed;

       /* if (Time.timeScale == 1)
        {
            playerRotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, baseSpeed * Time.deltaTime);
            transform.RotateAround(player.position, Vector3.down, adjustedSpeed * Time.deltaTime);  
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
