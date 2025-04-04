using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncreaseSpeed : UpgradeParent
{
    private int add = 50;
    [SerializeField] TextMeshProUGUI levelTxt;

    private void Start()
    {
        //Set text on screen to level of speed owned
        levelTxt.text = "Level: " + scriptObj.level.ToString();
    }

    public override void ActivateUpgrade()
    {
        //If level is 0
        if(scriptObj.level == 0)
        {
            //Add affect on speed to player speed
            playerMovement.moveSpeed += scriptObj.affectOnSpeed;
        }
        else
        {
            //if level is not 0, increase the affect on speed and apply to player
            scriptObj.affectOnSpeed += add;
            playerMovement.moveSpeed += scriptObj.affectOnSpeed;
        }

        //Increase the upgrade's level, save
        IncreaseLevel();
        saveData.SaveSpeedUpgrade();
    }

    public override void IncreaseLevel()
    {
        base.IncreaseLevel();

        //Set text to reflect level
        levelTxt.text = "Level: " + scriptObj.level.ToString();
    }
}
