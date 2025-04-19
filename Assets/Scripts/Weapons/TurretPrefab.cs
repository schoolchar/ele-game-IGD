using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPrefab : MonoBehaviour
{
    //time alive
    public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}