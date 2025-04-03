using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MaxHP : UpgradeParent
{
    private int addition = 2;
    [SerializeField] private TextMeshProUGUI leveltxt;

    private void Start()
    {
        leveltxt.text = "Level: " + scriptObj.level.ToString();
    }

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
        saveData.SaveHealthUpgrade();
    }

    public override void IncreaseLevel()
    {
        base.IncreaseLevel();
        leveltxt.text = "Level: " + scriptObj.level.ToString();
    }
}
