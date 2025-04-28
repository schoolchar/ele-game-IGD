using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCheckDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log("OBJECT IS DESTROYED");
    }

    private void OnEnable()
    {
        Debug.Log("OBJECT IS ENABLED");
    }
}
