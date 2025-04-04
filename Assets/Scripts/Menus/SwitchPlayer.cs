using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
    Elephant elephantComp;
    Lion lionComp;
    BasicShoot baseComp;
    MeshRenderer playerMesh;

    public Color[] playerColors;
    private void Start()
    {
        //Get the types of player characters' scripts on the player
        elephantComp = FindAnyObjectByType<Elephant>();
        lionComp = FindAnyObjectByType<Lion>();
        baseComp = FindAnyObjectByType<BasicShoot>();

        playerMesh = GameObject.FindWithTag("PlayerMesh").GetComponent<MeshRenderer>();
    }

    //Player chooses Lion character
    public void ActivateLion()
    {
        //Enable lion, disable everything else
        lionComp.enabled = true;
        elephantComp.enabled = false;
        baseComp.enabled = false;

        playerMesh.material.color = playerColors[0];
    }

    //Player chooses elephant character
    public void ActivateElephant()
    {
        //Enable elephant. disable everything else
        lionComp.enabled = false;
        elephantComp.enabled = true;
        baseComp.enabled = false;

        playerMesh.material.color = playerColors[1];
    }

    //Player chooses base character, temp while we finish all player characters
    public void ActivateBase()
    {
        //enable base, disable everything else
        lionComp.enabled = false;
        elephantComp.enabled = false;
        baseComp.enabled = true;

        playerMesh.material.color = playerColors[2];
    }

}
