using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBallsAroundObject : MonoBehaviour
{
    // Prefab of the ball to juggle (use prefab monkeyBall)
    public GameObject ballPrefab;

    // Number of balls to spawn and juggle
    public int numberOfBalls = 3;

    // List to store spawned ball GameObjects
    private List<GameObject> balls = new List<GameObject>();

    // Object to rotate around
    public Transform centerObject;

    // Radius of the rotation
    public float radius = 5.0f;

    // Speed of rotation
    public float speed = 50.0f;

    void Start()
    {
        SpawnBalls();
        Juggle();
    }

    void Update()
    {
        RotateBalls();
    }

    //Spawn monkey's  juggling balls when monkey is enabled
    void SpawnBalls()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            // Instantiate a ball and make it child
            GameObject ball = Instantiate(ballPrefab, centerObject.position, Quaternion.identity, centerObject);

            // Adds ball to the list
            balls.Add(ball);
        }
    } //END SpawnBalls()

    void Juggle()
    {
        // Puts balls around the center object
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] != null)
            {
                float angle = i * Mathf.PI * 2 / balls.Count; // Divide the circle into equal parts
                Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                balls[i].transform.localPosition = offset; // Use localPosition since balls are children
            }
        }
    }

    void RotateBalls()
    {
        // Rotate each ball around the center object
        foreach (GameObject ball in balls)
        {
            if (ball != null)
            {
                // Rotate the ball around the center object
                ball.transform.RotateAround(centerObject.position, Vector3.up, speed * Time.deltaTime);
            }
        }
    }
}