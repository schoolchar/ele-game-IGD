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

        levelTxt.text = "Level: " + scriptObj.level.ToString();
    }

    public override void ActivateUpgrade()
    {
        if(scriptObj.level == 0)
        {
            playerMovement.moveSpeed += scriptObj.affectOnSpeed;
        }
        else
        {
            scriptObj.affectOnSpeed += add;
            playerMovement.moveSpeed += scriptObj.affectOnSpeed;
        }

        IncreaseLevel();
        saveData.SaveSpeedUpgrade();
    }

    public override void IncreaseLevel()
    {
        base.IncreaseLevel();
        levelTxt.text = "Level: " + scriptObj.level.ToString();
    }
}
