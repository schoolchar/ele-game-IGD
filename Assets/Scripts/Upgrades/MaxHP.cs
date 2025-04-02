using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHP : UpgradeParent
{
    private int addition = 2;
    public override void ActivateUpgrade()
    {
        if(scriptObj.level == 0)
        {
            //Increase the maximum health the player can have
            playerHealth.maxHealth += scriptObj.affectOnHealth;
        }
       else
        {
            scriptObj.affectOnHealth += addition;
            playerHealth.maxHealth += scriptObj.affectOnHealth;
            addition *= 2;
            
        }

        IncreaseLevel();
    }
}
