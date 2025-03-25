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
        elephantComp = FindAnyObjectByType<Elephant>();
        lionComp = FindAnyObjectByType<Lion>();
        baseComp = FindAnyObjectByType<BasicShoot>();

        playerMesh = GameObject.FindWithTag("PlayerMesh").GetComponent<MeshRenderer>();
    }

    public void ActivateLion()
    {
        lionComp.enabled = true;
        elephantComp.enabled = false;
        baseComp.enabled = false;

        playerMesh.material.color = playerColors[0];
    }

    public void ActivateElephant()
    {
        lionComp.enabled = false;
        elephantComp.enabled = true;
        baseComp.enabled = false;

        playerMesh.material.color = playerColors[1];
    }

    public void ActivateBase()
    {
        lionComp.enabled = false;
        elephantComp.enabled = false;
        baseComp.enabled = true;

        playerMesh.material.color = playerColors[2];
    }

}
