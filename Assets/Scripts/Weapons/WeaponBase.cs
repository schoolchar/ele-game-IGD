using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    //All weapons should inherit from this class
    //Do not fill in the methods here, they are intentionally left blank
    //This is so that the children can define the behavior for it, but other scripts can easily find and activate the behavior of each weapon because they are not 
    //Looking for a bunch of seperate scripts
    //You may include different functions for the weapons for the different behavior, just make sure to be able to start the behavior and activate any relevant objects from the ActivateThisWeapons() method

    public virtual void ActivateThisWeapon()
    {
        //Do not put anything here
        //Make each weapon script inherit from this script and 
        //Among the methods in that script, include :
        //public override void ActivateThisWeapon() {
        //Within this version of the method, you can define the specific weapon behavior in some way, as well as other methods that belong to the subclass only

        //}
    }

    
}
