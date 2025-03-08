using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    private int baseValue;

    public int GetValue()
    {
        return baseValue;
    }
}
