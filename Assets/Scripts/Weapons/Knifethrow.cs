using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

public class Knifethrow : WeaponBase
{
    [SerializeField] ChooseWeapons chooseWeapons;

    // The layer mask for the enemy
    public LayerMask enemyLayerMask;

    // The prefab of the sphere to shoot
    public GameObject knife;

    // The speed of the projectile
    public float projectileSpeed = 20.0f;
    float oldSpeed = 20.0f;
    float speedUpgrade = 1.25f;

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
        Debug.Log("Knife activated");
        if (!hasKnife)
        {
            hasKnife = true;
            InvokeRepeating("TimeShooting", 0f, fireRate); 
           // StartCoroutine(TimeShooting());
            knifeActive = true;
        }
        else
        {
            projectileSpeed += 1.5f;
        }
        
    }

    void Start()
    {
        knifeSound = GetComponent<AudioSource>();
        chooseWeapons = FindAnyObjectByType<ChooseWeapons>();
        KnifeLevel = chooseWeapons.allWeaponsData[1].level; //Gets the level of knife throw
        oldKnifeLevel = KnifeLevel;
    }
    void Update()
    {
        KnifeLevel = chooseWeapons.allWeaponsData[1].level; //Updates the level of the knife throw
        
       /* if (Time.time > nextFireTime)
        {
            GameObject[] nearestEnemy = FindNearestEnemy(KnifeLevel);
            if (nearestEnemy != null && nearestEnemy.Length > 0 && knife == true)
            {
                Debug.Log("Knife thrown");
                foreach (GameObject enemy in nearestEnemy)
                {
                    ShootAt(enemy);
                }
                nextFireTime = Time.time + 1.0f / fireRate;
            }
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        DoDamage(collision);
    }

   /* GameObject FindNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Mathf.Infinity, enemyLayerMask);
        foreach (Collider hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = hitCollider.gameObject;
            }
        }
        Debug.Log("Nearest enemy = " + nearestEnemy);
        return nearestEnemy;
    }*/

    GameObject[] FindNearestEnemy(int count)
    {
        List<GameObject> nearestEnemy = new List<GameObject>();
        float[] nearestDistance = new float[count];
        for (int i = 0; i < count; i++)
        {
            nearestDistance[i] = Mathf.Infinity;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Mathf.Infinity, enemyLayerMask);
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

    void ShootAt(GameObject target)
    {
       /* if(oldKnifeLevel != KnifeLevel)
        {
            oldKnifeLevel++;
            projectileSpeed = oldSpeed * speedUpgrade;
        }

        oldSpeed = projectileSpeed;*/

        GameObject projectile = Instantiate(knife, transform.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            rb.velocity = direction * projectileSpeed;
        }
    }

    void DoDamage(Collision _collision)
    {
        if (_collision.gameObject.layer == 8)
        {
            _collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            transform.position = player.transform.position;
        }
    }

    void TimeShooting()
    {
       // yield return new WaitForSeconds(fireRate);
        GameObject[] nearestEnemy = FindNearestEnemy(KnifeLevel);
        if (nearestEnemy != null)
        {
            Debug.Log("Knife thrown");
            foreach (GameObject enemy in nearestEnemy)
            {
                knifeSound.Play();
                ShootAt(enemy);
            }
            nextFireTime = Time.time + 1.0f / fireRate;
            Array.Clear(nearestEnemy, 0, nearestEnemy.Length);
        }
        
        // StartCoroutine(TimeShooting());
    }

   /* void TimeShooting()
    {
        Debug.Log("Knife");
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null && hasKnife == true)
        {
            Debug.Log("Conditions for knife throw met");
            ShootAt(nearestEnemy);
            nextFireTime = Time.time + 1.0f / fireRate;
        }
    }*/
}