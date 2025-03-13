using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magnetism : UpgradeParent
{
    SphereCollider magCollider;
    public List<GameObject> pickup;
    public GameObject player;
    bool pulling;

    private void Start()
    {
        magCollider = GetComponent<SphereCollider>();
        float magRadius = scriptObj.affectOnMag; //variable to ease typing + neatness
        magCollider.radius = magRadius;
    }

    //this function will take the above info and check if pickup items cross the radius
    //then the objects will start moving to the player
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup")) 
        {
            pickup.Add(other.gameObject);
            pulling = true;
        }
    }

    private void Update()
    {
        if (pulling)
        {
            for (int i = 0; i < pickup.Count; i++)
            {
                pickup[i].transform.position = Vector3.MoveTowards(pickup[i].transform.position, player.transform.position, scriptObj.magSpeed * Time.deltaTime);
            }
        }
    }
}
