using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monkey : MonoBehaviour
{
    //prefab of balls
    public GameObject balls;

    // put infront of player
    public Vector3 positionOffset = new Vector3(0, 0, 2);

    public GameObject playerobj;
    void Start()
    {
        // Calculate position
        Vector3 spawnPosition = transform.position + transform.TransformDirection(positionOffset);

        // Instantiate the prefab infront of player, using calculated position
        GameObject instantiatedObject = Instantiate(balls, spawnPosition, Quaternion.identity);

        //makes child of player for moving
        instantiatedObject.transform.SetParent(playerobj.transform);
    }

}