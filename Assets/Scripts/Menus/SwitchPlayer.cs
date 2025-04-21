using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
    public Elephant elephantComp;
    public Lion lionComp;
    public SeaLion seaLionComp;
    public Monkey monkeyComp;
    public BasicShoot baseComp;

    public CharacterID[] playerModels;
    public GameObject characterActive;


    public Color[] playerColors;
    private void Start()
    {
        //Get the types of player characters' scripts on the player
        elephantComp = FindAnyObjectByType<Elephant>();
        lionComp = FindAnyObjectByType<Lion>();
        baseComp = FindAnyObjectByType<BasicShoot>();
        seaLionComp = FindAnyObjectByType<SeaLion>();
        monkeyComp = FindAnyObjectByType<Monkey>();

        playerModels = FindObjectsByType<CharacterID>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        characterActive = FindAnyObjectByType<PlayerMovement>().characterActive;

       // playerMesh = GameObject.FindWithTag("PlayerMesh").GetComponent<MeshRenderer>();
    }

    //Player chooses Lion character
    public void ActivateLion()
    {
        //Enable lion, disable everything else
        lionComp.enabled = true;
        elephantComp.enabled = false;
        baseComp.enabled = false;
        monkeyComp.enabled = false;
        seaLionComp.enabled = false;

        characterActive.SetActive(false);
        for (int i = 0; i < playerModels.Length; i++)
        {
            if (playerModels[i].animal == "Lion")
            {
                playerModels[i].gameObject.SetActive(true);
            }
        }


        //playerMesh.material.color = playerColors[0];
    }

    //Player chooses elephant character
    public void ActivateElephant()
    {
        //Enable elephant. disable everything else
        lionComp.enabled = false;
        elephantComp.enabled = true;
        seaLionComp.enabled = false;
        monkeyComp.enabled = false;
        baseComp.enabled = false;

        characterActive.SetActive(false);
        for (int i = 0; i < playerModels.Length; i++)
        {
            if (playerModels[i].animal == "Elephant")
            {
                playerModels[i].gameObject.SetActive(true);
            }
        }

        //playerMesh.material.color = playerColors[1];
    }

    public void ActivateSeal()
    {
        seaLionComp.enabled = true;
        lionComp.enabled = false;
        monkeyComp.enabled = false;
        baseComp.enabled = false;
        elephantComp.enabled = false;

        characterActive.SetActive(false);
        for (int i = 0; i < playerModels.Length; i++)
        {
            if (playerModels[i].animal == "Seal")
            {
                playerModels[i].gameObject.SetActive(true);
            }
        }
    }

    public void ActivateMonkey()
    {
        monkeyComp.enabled = true;
        seaLionComp.enabled = false;
        lionComp.enabled = false;
        baseComp.enabled = false;
        elephantComp.enabled = false;

        characterActive.SetActive(false);
        for (int i = 0; i < playerModels.Length; i++)
        {
            if (playerModels[i].animal == "Monkey")
            {
                playerModels[i].gameObject.SetActive(true);
            }
        }
    }

    

}
