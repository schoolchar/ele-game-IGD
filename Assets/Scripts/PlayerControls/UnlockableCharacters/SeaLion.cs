using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeaLion : MonoBehaviour
{
    private float preThrow = 3.5f;
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
    [SerializeField] private GameObject sealObj;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0f, 0f, 1.5f);
        offsetS = new Vector3(0f, 2.0f, 1.0f);
        staticBall = Instantiate(ballPrefabS, currPosition + offsetS, player.transform.rotation);
        staticBall.transform.parent = sealObj.transform;
        staticBall.SetActive(true);
        StartCoroutine(TimeBall());

    }

    // Update is called once per frame
    void Update()
    {
        TrackPosition();
        if (thrownBall != null)
        {
            distance = Vector3.Distance(thrownBall.transform.position, launchPosition); //check the distance
            if (distance > maxDistance)
            {
                //Debug.Log("Distance reached.");
                distCond = true;
            }
        }
        
       


        //ensure this doesn't infinitely repeat

        if (distCond) //conditionals for missing a throw
        {
         //   Debug.Log("Distance reached, inflating");
            Destroy(thrownBall.gameObject);
            thrownBall = null;
            staticBall.SetActive(true);
            preThrow = 2.5f;
            freezeTimer = false;
            distCond = false;
        }

        if (targetHit) //conditionals for hitting a throw
        {
            //Debug.Log("target hit, bouncing back");
            thrownBall.transform.position = Vector3.MoveTowards(thrownBall.transform.position, currPosition, (speed * 2) * Time.deltaTime);
            if (distance < 3f)
            {
                Destroy(thrownBall.gameObject);
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
        staticBall.transform.position = currPosition + offsetS; //Null error, says this object has been destroyed
    }

    /// <summary>
    /// Throw ball seal is carrying
    /// </summary>
    void ThrowBall()
    {
        //Debug.Log("Throwing");
        launchPosition = currPosition;
        staticBall.SetActive(false); //get rid of the one above head
        thrownBall = Instantiate(ballPrefabS, launchPosition + offset, spawnPt.transform.rotation).GetComponent<Rigidbody>(); //put ball infront of player
        thrownBall.velocity = spawnPt.transform.forward * speed;
    } //END ThrowBall()

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    /// <summary>
    /// Repeat ball
    /// </summary>
    IEnumerator TimeBall()
    {
        yield return new WaitForSeconds(preThrow);

        ThrowBall();
        StartCoroutine(TimeBall());
    } //END TimeBall()
}
