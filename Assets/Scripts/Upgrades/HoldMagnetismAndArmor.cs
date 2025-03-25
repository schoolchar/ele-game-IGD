using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldMagnetismAndArmor : MonoBehaviour
{
    public void ActivateMagnetism()
    {
        FindAnyObjectByType<Magnetism>().ActivateUpgrade();
    }

    public void ActivateArmor()
    {
        //Hold functionality for turning on armor
    }
}
