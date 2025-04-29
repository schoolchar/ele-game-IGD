using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeParent : MonoBehaviour, IUpgrade
{
    protected PlayerHealth playerHealth;
    [SerializeField] protected PlayerMovement playerMovement;
    protected SaveData saveData;
    public UpgradeScriptObj scriptObj;
    [SerializeField] protected TextMeshProUGUI purchaseText;
    protected StoreMenuScript storeMenu;

    public void Awake()
    {
        //Set values
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        saveData = playerHealth.gameObject.GetComponent<SaveData>();
        playerMovement = playerHealth.gameObject.GetComponent<PlayerMovement>();
        storeMenu = FindAnyObjectByType<StoreMenuScript>();

        ChangeCostText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnContact(collision);
    }


    public virtual void OnContact(Collision _collider)
    {
        //Check for player layer, checks if player has collided w/ upgrade
        if (_collider.collider.gameObject.layer == 7)
        {
            ActivateUpgrade();
        }
    }

    public virtual void ActivateUpgrade() 
    {
        ;
    }

    public virtual void IncreaseLevel()
    {
        //Increase level of upgrade
        scriptObj.level++;
    }

    protected void ChangeCostText()
    {
        purchaseText.text = "Buy - $" + scriptObj.cost;
    }
    protected void ChangeCostBasedOnLevel()
    {
        playerHealth.money -= scriptObj.cost;
        storeMenu.ShowMoneyText();
        int _modVal = (scriptObj.level / 2) + scriptObj.cost;
        scriptObj.cost += _modVal;
        ChangeCostText(); ;
    }
   

}
