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
        //Set text on screen to level
        leveltxt.text = "Level: " + scriptObj.level.ToString();
    }

    public override void ActivateUpgrade()
    {
        if(playerHealth.money >= scriptObj.cost)
        {
            //if level is 0
            if (scriptObj.level == 0)
            {
                //Increase the maximum health the player can have
                playerHealth.maxHealth += scriptObj.affectOnHealth;
            }
            else
            {
                //If level is not 0, increase the affect on health and add to player's max health
                scriptObj.affectOnHealth += addition;
                playerHealth.maxHealth += scriptObj.affectOnHealth;
                addition *= 2;

            }

            IncreaseLevel();
            //Change cost for next level
            ChangeCostBasedOnLevel();
            //Save level increase
            saveData.SaveHealthUpgrade();
        }
        else
        {
            storeMenu.NotEnoughMoney();
        }
       
    }

    public override void IncreaseLevel()
    {
        base.IncreaseLevel();

        //Change text on screen
        leveltxt.text = "Level: " + scriptObj.level.ToString();
    }
}
