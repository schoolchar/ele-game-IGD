using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeForceSteal : UpgradeParent
{
    int add = 1;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        //Set text on screen to level
        levelText.text = "Level: " + scriptObj.level.ToString();
    }

    public override void ActivateUpgrade()
    {
        //If level is 0
        if(scriptObj.level == 0)
        {
            //Set life force to true and add to health added per enemy killed
            playerHealth.lifeForce = true;
            playerHealth.healthPerEnemy += scriptObj.affectOnHealth;
        }
        else
        {
            //If not level 0, increase the amount of health that is gained per enemy killed by 1
            scriptObj.affectOnHealth += add;
            playerHealth.healthPerEnemy += scriptObj.affectOnHealth;
        }
       
        //Increase upgrade level
        IncreaseLevel();
        //Save
        saveData.SaveLifeForceUpgrade();

    }

    public override void IncreaseLevel()
    {
        base.IncreaseLevel();
        //Change text on screen
        levelText.text = "Level: " + scriptObj.level.ToString();
    }
}
