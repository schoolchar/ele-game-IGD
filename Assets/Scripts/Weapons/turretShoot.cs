using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shotSpeed = 20f;
    [SerializeField] private float shootInterval = 1f;

    private float shootTimer;

    void Update()
    {
        // Update the shoot timer
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                // Shoot at the nearest enemy
                Shoot(nearestEnemy);
                shootTimer = shootInterval; // Reset the shoot timer
            }
        }
    }

    /// <summary>
    /// Find the closest enemy to target shoot, using distance form turret
    /// </summary>
    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    } //END FindNearestEnemy

    /// <summary>
    /// Instantiate and shoot bullet from turret
    /// </summary>
    private void Shoot(GameObject target)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector3 direction = (target.transform.position - firePoint.position).normalized;
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = direction * shotSpeed;
    } //END Shoot()
}
