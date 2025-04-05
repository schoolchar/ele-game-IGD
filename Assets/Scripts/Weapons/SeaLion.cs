using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeaLion : MonoBehaviour
{
    private float preThrow = 5.0f;
    public GameObject ballPrefabS;
    public Rigidbody ballPrefabT;
    public GameObject player;
    public GameObject spawnPt;
    private GameObject staticBall;
    private Rigidbody thrownBall;
    public static bool targetHit = false;
    public static float speed = 10f;
    private Vector3 offsetS;
    private Vector3 offset;
    private Vector3 currPosition;
    private Vector3 launchPosition;
    private float maxDistance = 25f;
    public float distance;
    private bool distCond = false;
    private bool freezeTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0f, 0f, 1.0f);
        offsetS = new Vector3(0f, 2.0f, 1.0f);
        staticBall = Instantiate(ballPrefabS, currPosition + offsetS, player.transform.rotation);
        staticBall.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        TrackPosition();
        if (thrownBall != null)
        {
            Debug.Log("calculating distance");
            distance = Vector3.Distance(thrownBall.transform.position, launchPosition); //check the distance
            if (distance > maxDistance)
            {
                Debug.Log("Distance reached.");
                distCond = true;
            }
        }
        
        if (preThrow >= 0 && !freezeTimer)
        {
            preThrow -= Time.deltaTime;
        }

        if (preThrow <= 0)
        {
            ThrowBall();
            freezeTimer = true;
            preThrow = 1f;
        }
        //ensure this doesn't infinitely repeat

        if (distCond) //conditionals for missing a throw
        {
            Debug.Log("Distance reached, inflating");
            Destroy(thrownBall);
            thrownBall = null;
            staticBall.SetActive(true);
            preThrow = 4.0f;
            freezeTimer = false;
            distCond = false;
        }

        if (targetHit) //conditionals for hitting a throw
        {
            Debug.Log("target hit, bouncing back");
            thrownBall.transform.position = Vector3.MoveTowards(thrownBall.transform.position, player.transform.position, (speed * 3) * Time.deltaTime);
            if (distance > 1.5f)
            {
                Destroy(thrownBall);
                thrownBall = null;
                staticBall.SetActive(true);
                preThrow = 1.0f;
                freezeTimer = false;
            }
            targetHit = false;
        }
    }

    void TrackPosition()
    {
        currPosition = player.transform.position;
        staticBall.transform.position = currPosition + offsetS;
    }

    void ThrowBall()
    {
        Debug.Log("Throwing");
        launchPosition = currPosition;
        staticBall.SetActive(false); //get rid of the one above head
        thrownBall = Instantiate(ballPrefabT, launchPosition + offset, spawnPt.transform.rotation); //put ball infront of player
        thrownBall.velocity = -transform.forward * speed;
    }

}
