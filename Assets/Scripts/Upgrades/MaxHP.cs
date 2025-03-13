using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHP : UpgradeParent
{
    public override void ActivateUpgrade()
    {
        //Increase the maximum health the player can have
        playerHealth.maxHealth += scriptObj.affectOnHealth;
    }
}
