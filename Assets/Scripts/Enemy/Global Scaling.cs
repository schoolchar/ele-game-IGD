using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScaling : MonoBehaviour
{
    
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        DamageScaling();
        HealthScaling();
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        Debug.Log((int)timer);
    }

    private void DamageScaling()
    {


        if ((int)timer % 30 == 0)
        {
            Debug.Log("Player damage scaled");
            EnemyHealth.maxDmg += 2;
            EnemyHealth.minDmg += 2;
        }
    }

    private void HealthScaling()
    {

        if ((int)timer % 30 == 0)
        {
            Debug.Log("Enemy health scaled");
            EnemyHealth.maxHealth += 3;
        }
    }
}
