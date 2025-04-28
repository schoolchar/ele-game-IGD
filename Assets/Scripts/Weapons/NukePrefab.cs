using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NukePrefab : MonoBehaviour
{
    //scaling 
    public float nukeLevel = 0;

    // Target scale 
    public float targetScale = 11f;

    public float targetScaleDestroyBase = 10;
    public float targetScaleDestroy;

    // Speed of scaling
    public float scaleSpeed = 1f;

    public AudioSource nukeSound;
    private bool isInGameScene;

    private void Update()
    {
        //increae scale over time using scale speed, goal set to targetScale
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * targetScale, Time.deltaTime * scaleSpeed);

        // uses taget scale destroy because it never reaches the set scale, idk why but setting it 1 above works
        if (transform.localScale.x >= targetScaleDestroy && transform.localScale.y >= targetScaleDestroy && transform.localScale.z >= targetScaleDestroy)
        {
            //destroys
            Destroy(gameObject);
        }

        //gets scene name
        Scene currentScene = SceneManager.GetActiveScene();
        isInGameScene = currentScene.name == "GameScene";


        //If player is in the game scene
        if (isInGameScene == true)
        {
            nukeSound.Play();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if hit enemy
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(50);
        }
    }

    void levelup()
    {
        targetScaleDestroy = targetScaleDestroyBase + nukeLevel;
        nukeLevel ++;
    }
}
