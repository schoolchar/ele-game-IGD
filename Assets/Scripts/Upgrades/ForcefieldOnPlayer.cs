using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldOnPlayer : MonoBehaviour
{
   public bool forcefieldActive; //Keeps track of if able to use bw saving
    [SerializeField] private GameObject forceFieldObj;
    public float timeBwActive; //Scalable factor, increase in level decreases this NOT IMPLEMENTED YET
    public float deactivateTime = 3;
    [SerializeField] private SaveData saveData;

    private void Start()
    {
        deactivateTime = saveData.forcefield.deactivateTimeFF;
        if(saveData.forcefield.level == 0)
        {
            forcefieldActive = false;
        }
        else
        {
            forcefieldActive = true;
        }
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
   public  IEnumerator TimeForcefield()
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
