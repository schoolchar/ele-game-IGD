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

    public override void ActivateUpgrade()
    {
        if(scriptObj.level == 0)
        {
            forceFieldScript.ActivateForcefield();
        }
        else
        {
            forceFieldScript.deactivateTime += levelChange;
            scriptObj.deactivateTimeFF = (int)forceFieldScript.deactivateTime;
            //saveData.SaveForcefieldUpgrade();
        }

        IncreaseLevel();
        
    }

    public override void IncreaseLevel()
    {
        
         base.IncreaseLevel();

         //Change text on screen
         levelText.text = "Level: " + scriptObj.level.ToString();
         saveData.SaveForcefieldUpgrade();
        
       
    }
}
