using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class LargeHammer : MonoBehaviour
{
    public float waitTimer;
    public GameObject hammer;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        waitTimer = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        waitTimer -= Time.deltaTime;
        Vector3 endRotate = new Vector3(45, 0, 0);
        if (waitTimer < 0)
        {
            Instantiate(hammer, player.position, Quaternion.Euler(-45, 0, 0));
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, endRotate, Time.deltaTime);
            Destroy(hammer);
        }
    }
}
