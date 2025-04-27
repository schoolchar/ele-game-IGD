using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Ringoffire : WeaponBase
{
    [SerializeField] ChooseWeapons chooseWeapons;
    public Transform player;
   // private Quaternion playerRotation;
    public int RingoffireLevel;
    public float distance = 2.0f;
    public float baseSpeed = 5f;
    float adjustedSpeed;
    float Upgrade = 1.25f;

    //private Vector3 lastPlayerPosition;
   // private float playerSpeed;
    public bool fireActive;
    private int oldRingoffireLevel;
    public AudioSource fireSound;

    // Start is called before the first frame update
    void Start()
    {
        //Ring of fire game object ignores specific game objects depending on their layer
        Physics.IgnoreLayerCollision(10, 9, true);
        Physics.IgnoreLayerCollision(10, 10, true);
        Physics.IgnoreLayerCollision(10, 12, true);
        Physics.IgnoreLayerCollision(10, 13, true);

        chooseWeapons = FindAnyObjectByType<ChooseWeapons>();
        RingoffireLevel = chooseWeapons.allWeaponsData[0].level; //Gets the level of ring of fire
        oldRingoffireLevel = RingoffireLevel;
    }
    public override void ActivateThisWeapon()
    {
        // player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        //  lastPlayerPosition = player.position;
        chooseWeapons = FindAnyObjectByType<ChooseWeapons>();
        RingoffireLevel = chooseWeapons.allWeaponsData[0].level; //Gets the level of ring of fire
        oldRingoffireLevel = RingoffireLevel;
        fireActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the level of the ring of fire
         
        
        //Debug.Log("Fire activated");
        if (fireActive)
        {
            //UpdatePlayerSpeed();
            RingoffireLevel = chooseWeapons.allWeaponsData[0].level;
            FireRing();
        }
       /* else
        {
           // baseSpeed += (baseSpeed + playerSpeed) * 1.5f;
           // orbitSpeed += (orbitSpeed + playerSpeed) * 1.5f;
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitEnemy(collision);
    }
    void HitEnemy(Collision _collision)
    {
        //If the ring of fire collides with an enemy, enemy takes damage
        if (_collision.gameObject.layer == 8)
        {
            //Debug.Log("Ring of fire has hit enemy");
            _collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }

   /* void UpdatePlayerSpeed()
    {
        // Calculate the player's speed based on movement since the last frame
        Vector3 playerMovement = player.position - lastPlayerPosition;
        playerSpeed = playerMovement.magnitude / Time.deltaTime;
        lastPlayerPosition = player.position;
    }*/
    void FireRing()
    {
        adjustedSpeed = baseSpeed;

        if(oldRingoffireLevel != RingoffireLevel)
        {
            //Debug.Log("Level up");
            oldRingoffireLevel++;
            adjustedSpeed = baseSpeed * Upgrade;
        }

       // oldSpeed = adjustedSpeed;
        //Speed of rotation
        baseSpeed = adjustedSpeed;

        //Rotation angle
        float angle = Time.time * adjustedSpeed;

        //Updates the player's x,y, and z postion 
        float x = player.position.x + Mathf.Cos(angle) * distance;
        float y = player.position.y;
        float z = player.position.z + Mathf.Sin(angle) * distance;

        //Updates the postion of the ring of fire based on the player's position
        transform.position = new Vector3(x, y, z);

       /* if (Time.timeScale == 1)
        {
            playerRotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, baseSpeed * Time.deltaTime);
            transform.RotateAround(player.position, Vector3.down, adjustedSpeed * Time.deltaTime);  
        }*/

        // Rotate around the player with the adjusted speed
       // transform.RotateAround(player.position, Vector3.down, adjustedSpeed * Time.deltaTime);

        // Maintain the specified distance from the player
       // transform.position = player.position + (transform.position - player.position).normalized * distance;
    }

    // Method to set the distance dynamically
   /* public void SetDistance(float newDistance)
    {
        distance = newDistance;
    }

    // Method to set the base speed dynamically
    public void SetBaseSpeed(float newBaseSpeed)
    {
        baseSpeed = newBaseSpeed;
    }*/
}
