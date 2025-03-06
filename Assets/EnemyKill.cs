using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
