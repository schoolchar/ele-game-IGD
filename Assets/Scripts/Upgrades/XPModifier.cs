using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPModifier : UpgradeParent
{
    public override void ActivateUpgrade()
    {
        playerHealth.xpMod = true;
        playerHealth.xpModVal = scriptObj.affectOnXP;
    }
}
