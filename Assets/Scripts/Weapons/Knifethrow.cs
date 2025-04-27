using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using Unity.VisualScripting.InputSystem;

public class Knifethrow : WeaponBase
{
    [SerializeField] ChooseWeapons chooseWeapons;

    // The layer mask for the enemy
    public LayerMask enemyLayerMask;

    // The prefab of the knife to shoot
    public GameObject knife;

    // The speed of the projectile
    public float projectileSpeed = 20.0f;
   // float oldSpeed = 20.0f;
   // float speedUpgrade = 1.25f;

    // The rate of fire
    public float fireRate = 1.0f;

    private float nextFireTime = 0.0f;

    public bool hasKnife = false;

    bool knifeActive;

    // The level variable to determine the number of enemies to target
    public int KnifeLevel;
    private int oldKnifeLevel;
    public AudioSource knifeSound;

    //get component player script
    [SerializeField] private GameObject player;

    public override void ActivateThisWeapon()
    {
        knifeSound = GetComponent<AudioSource>();
        chooseWeapons = FindAnyObjectByType<ChooseWeapons>();
        KnifeLevel = chooseWeapons.allWeaponsData[1].level; //Gets the level of knife throw
        oldKnifeLevel = KnifeLevel;

        //Debug.Log("Knife activated");
        if (!hasKnife)
        {
            hasKnife = true;
           // InvokeRepeating("TimeShooting", 0f, fireRate); 
            knifeActive = true;
        }
        else
        {
            projectileSpeed += 1.5f;
        }
        
    }

    void Start()
    {
        //Knife game object ignores specific game objects depending on their layer
        Physics.IgnoreLayerCollision(9, 9, true);
        Physics.IgnoreLayerCollision(9, 7, true);
        Physics.IgnoreLayerCollision(9, 10, true);
        Physics.IgnoreLayerCollision(9, 12, true);
        Physics.IgnoreLayerCollision(9, 13, true);

        knifeSound = GetComponent<AudioSource>();
        chooseWeapons = FindAnyObjectByType<ChooseWeapons>();
        KnifeLevel = chooseWeapons.allWeaponsData[1].level; //Gets the level of knife throw
        oldKnifeLevel = KnifeLevel;
    }
    void Update()
    {
        //Updates the level of the knife
        //KnifeLevel = chooseWeapons.allWeaponsData[1].level; 
        
        if (Time.time > nextFireTime)
        {
            TimeShooting();
        }
    }

   

    //Function that calculates the nearest enemy
    GameObject[] FindNearestEnemy(int count)
    {
        List<GameObject> nearestEnemy = new List<GameObject>();
        float[] nearestDistance = new float[count];
        for (int i = 0; i < count; i++)
        {
            nearestDistance[i] = Mathf.Infinity;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Mathf.Infinity, enemyLayerMask);
        
        if(hitColliders.Length == 0)
        {
            return null;
        }

        foreach (Collider hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            for (int i = 0; i < count; i++)
            {
                if (distance < nearestDistance[i])
                {
                    nearestDistance[i] = distance;
                    if (nearestEnemy.Count > i)
                    {
                        nearestEnemy[i] = hitCollider.gameObject;
                    }
                    else
                    {
                        nearestEnemy.Add(hitCollider.gameObject);
                    }
                    break;
                }
            }
        }

        return nearestEnemy.ToArray();
    }


    //Function that creates a knife and shoots at the target
    void ShootAt(GameObject target)
    {
        if(oldKnifeLevel != KnifeLevel)
        {
            oldKnifeLevel++;
           // projectileSpeed = oldSpeed * speedUpgrade;
        }

       // oldSpeed = projectileSpeed;

        GameObject projectile = Instantiate(knife, transform.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.LookAt(direction);
            rb.velocity = direction * projectileSpeed;
        }
    }

    /*Function that uses a list to call the ShootAt function on the nearest enemies equal to the knife level.
    For example, if the knife level is 1, only one knife is thrown. If the knife level is 2, two knifes 
    will be thrown if there is more then 1 enemy with the knifes shooting at the nearest 2 enemies.*/
    void TimeShooting()
    {
       // yield return new WaitForSeconds(fireRate);
        GameObject[] nearestEnemy = FindNearestEnemy(KnifeLevel);
        if (nearestEnemy != null)
        {
            //Debug.Log("Knife thrown");
            foreach (GameObject enemy in nearestEnemy)
            {
                knifeSound.Play();
                ShootAt(enemy);
            }
            nextFireTime = Time.time + 1.0f / fireRate;
            Array.Clear(nearestEnemy, 0, nearestEnemy.Length);
        }
        else
        {
            //Debug.Log("No enemies");
        }
        
        // StartCoroutine(TimeShooting());
    }

   
}