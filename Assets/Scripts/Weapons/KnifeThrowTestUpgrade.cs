using UnityEngine;
using System.Collections.Generic;

public class KnifethrowTestUpgrade : MonoBehaviour
{
    ChooseWeapons chooseWeapons;
    // The layer mask for the enemy
    public LayerMask enemyLayerMask;

    // The prefab of the sphere to shoot
    public GameObject knife;

    // The speed of the projectile
    public float projectileSpeed = 20.0f;

    // The rate of fire
    public float fireRate = 1.0f;

    private float nextFireTime = 0.0f;

    // The level variable to determine the number of enemies to target
    public int KnifeLevel;

    //get component player script
    [SerializeField] private GameObject player;

    void Start()
    {
       KnifeLevel = chooseWeapons.Knifelevel; 
    }
    void Update()
    {
        if (Time.time > nextFireTime)
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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        DoDamage(collision);
    }

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
}
