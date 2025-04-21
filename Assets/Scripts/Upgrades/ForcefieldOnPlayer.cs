using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldOnPlayer : MonoBehaviour
{
   public bool forcefieldActive; //Keeps track of if able to use bw saving
    [SerializeField] private GameObject forceFieldObj;
    public float timeBwActive; //Scalable factor, increase in level decreases this NOT IMPLEMENTED YET
    private float deactivateTime = 3;

    private void Start()
    {
        //In the event the forcefield is actiave between loads
        if(forcefieldActive)
        {
            StartCoroutine(TimeForcefield());
        }
    }

    //Set on activate, allows player to use forcefield
    public void ActivateForcefield()
    {
        forcefieldActive = true;
        StartCoroutine(TimeForcefield());
    }

    //Sets forcefield active
    IEnumerator TimeForcefield()
    {
        yield return new WaitForSeconds(timeBwActive);
        forceFieldObj.SetActive(true);
        StartCoroutine(DeActivateForcefield());
    }

    //Sets forcefield inactive
    IEnumerator DeActivateForcefield()
    {
        yield return new WaitForSeconds (deactivateTime);
        forceFieldObj.SetActive (false);
        StartCoroutine(TimeForcefield());
    }
}
