using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class XPModifier : UpgradeParent
{
    private int xpAdd = 2;
    [SerializeField] private TextMeshProUGUI leveltxt;


    private void Start()
    {
        leveltxt.text = "Level: " + scriptObj.level.ToString();
    }
    public override void ActivateUpgrade()
    {
        if(playerHealth.money >= scriptObj.cost)
        {
            if (scriptObj.level == 0)
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
            //Change cost for next level
            ChangeCostBasedOnLevel();
            saveData.SaveXPUpgrade();
        }
        else
        {
            storeMenu.NotEnoughMoney();
        }
        
    }

    public override void IncreaseLevel()
    {
        base.IncreaseLevel();
        leveltxt.text = "Level: " + scriptObj.level.ToString();
    }
}
