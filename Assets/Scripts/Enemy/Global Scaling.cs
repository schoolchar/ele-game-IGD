using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScaling : MonoBehaviour
{
    
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DamageScaling", 30f, 30f);
        InvokeRepeating("HealthScaling", 30f, 30f);
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer - (int)timer == 0)
            Debug.Log((int)timer);
    }

    private void DamageScaling()
    {
        Debug.Log("Player damage scaled");
        EnemyHealth.maxDmg += 2;
        EnemyHealth.minDmg += 2;
    }

    private void HealthScaling()
    {
        Debug.Log("Enemy health scaled");
        EnemyHealth.maxHealth += 3;

    }
}
