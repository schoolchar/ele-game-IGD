using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Forcefield : UpgradeParent
{ //Not a scalable upgrade right now

    ForcefieldOnPlayer forceFieldScript;
    [SerializeField] private TextMeshProUGUI levelText;
    private int levelChange = 1;

    private void Start()
    {
        forceFieldScript = FindAnyObjectByType<ForcefieldOnPlayer>();
        levelText.text = "Level: " + scriptObj.level.ToString();
    }

    /// <summary>
    /// Activate forcefield, start necessary functionalty
    /// </summary>
    public override void ActivateUpgrade()
    {
        //Check if player has enough money
        if(playerHealth.money >= scriptObj.cost)
        {
            if (scriptObj.level == 0)
            {
                forceFieldScript.ActivateForcefield();
            }
            else
            {
                forceFieldScript.deactivateTime += levelChange;
                scriptObj.deactivateTimeFF = (int)forceFieldScript.deactivateTime;
                //saveData.SaveForcefieldUpgrade();
            }

            //Increase level of upgrade
            IncreaseLevel();
            //Change cost for next level
            ChangeCostBasedOnLevel();
            saveData.SaveForcefieldUpgrade();
        }
        else
        {
            storeMenu.NotEnoughMoney();
        }
       
        
    } //END ActivateUpgrade()

    /// <summary>
    /// Increase upgrade level
    /// </summary>
    public override void IncreaseLevel()
    {
        
         base.IncreaseLevel();

         //Change text on screen
         levelText.text = "Level: " + scriptObj.level.ToString();
         saveData.SaveForcefieldUpgrade();
        
       
    } //END IncreaseLevel()
}
