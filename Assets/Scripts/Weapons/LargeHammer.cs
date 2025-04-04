using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class LargeHammer : MonoBehaviour
{
    public float waitTimer;
    public GameObject hammer;
    private GameObject hammerC;
    public Transform player;
    private Vector3 currPosition;
    // Start is called before the first frame update
    void Start()
    {
        waitTimer = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        TrackPosition();
        waitTimer -= Time.deltaTime;
        
        if (waitTimer <= 0)
        {
            Swing();
        }
        
    }

    void TrackPosition()
    {
        currPosition = player.position;
    }

    void Swing()
    {
        Vector3 endRotate = new Vector3(45, 0, 0);
        hammerC = Instantiate(hammer, currPosition, Quaternion.Euler(-45, 0, 0));
        Debug.Log("hammer created");
        hammerC.transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, endRotate, Time.deltaTime);
        if (hammerC != null)
        {
            Destroy(hammerC);
            waitTimer = 2.0f;
        }
    }
}
