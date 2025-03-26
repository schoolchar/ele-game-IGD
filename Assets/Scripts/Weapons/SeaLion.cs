using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SeaLion : MonoBehaviour
{
    private float preStart = 5.0f;
    public GameObject ballPrefabS;
    public GameObject ballPrefabT;
    public GameObject player; 
    private float inflate = 2.0f;
    private bool canThrow = true;
    public bool targetHit = false;
    private float speed = 15.0f;
    private Vector3 offsetS;
    private Vector3 offset;
    private Vector3 currPosition;
    private float maxDistance = 200.0f;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 0, 2.0f);
        offsetS = new Vector3(0, 1.0f, 0);
        Instantiate(ballPrefabS, player.transform.position + offsetS, player.transform.rotation);
        ballPrefabS.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        ballPrefabS.transform.position = player.transform.position + offsetS;
        currPosition = player.transform.position;
        if (preStart >= 0)
        {
            preStart -= Time.deltaTime;
        }
        
        if (canThrow & preStart <= 0)
        {
            ballPrefabS.SetActive(false); //get rid of the one above head
            Instantiate(ballPrefabT, currPosition + offset, player.transform.rotation); //put ball infront of player
            ballPrefabT.transform.Translate(Vector3.up * speed * Time.deltaTime); //throw it
            distance = Vector3.Distance(ballPrefabT.transform.position, currPosition); //check the distance
            canThrow = false; //ensure this doesn't infinitely repeat

            if (distance > maxDistance) //conditionals for missing a throw
            {
                Destroy(ballPrefabT); 
                ballPrefabS.SetActive(true);
                inflate -= Time.deltaTime; //cooldown for losing ball
                if(inflate <= 0.0)
                {
                    inflate = 2.0f;
                    canThrow = true;
                }
            }

            if (targetHit) //conditionals for hitting a throw
            {
                ballPrefabT.transform.position = Vector3.MoveTowards(ballPrefabT.transform.position, player.transform.position, speed * Time.deltaTime);
                if (Vector3.Distance(ballPrefabT.transform.position, player.transform.position) > 1.5f)
                {
                    Destroy(ballPrefabT);
                    ballPrefabS.SetActive(true);
                    canThrow = true;
                }
            }
            
        }
    }


}
