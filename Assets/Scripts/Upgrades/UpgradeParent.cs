using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeParent : MonoBehaviour, IUpgrade
{
    protected PlayerHealth playerHealth;
    [SerializeField] protected PlayerMovement playerMovement;
    protected SaveData saveData;
    public UpgradeScriptObj scriptObj;

    public void Awake()
    {
        //Set values
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        saveData = playerHealth.gameObject.GetComponent<SaveData>();
        playerMovement = playerHealth.gameObject.GetComponent<PlayerMovement>();
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


   

}
