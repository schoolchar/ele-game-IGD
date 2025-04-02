using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPModifier : UpgradeParent
{
    private int xpAdd = 2;
    public override void ActivateUpgrade()
    {
        if(scriptObj.level == 0)
        {
            playerHealth.xpMod = true;
            playerHealth.xpModVal = scriptObj.affectOnXP;
        }
        else
        {
            scriptObj.affectOnXP += xpAdd;
            playerHealth.xpModVal = scriptObj.affectOnXP;
        }
        
        IncreaseLevel();
        saveData.SaveXPUpgrade();
    }
}
