using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeParent : MonoBehaviour, IUpgrade
{
    protected PlayerHealth playerHealth;
    public UpgradeScriptObj scriptObj;

    void Start()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();
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


}
