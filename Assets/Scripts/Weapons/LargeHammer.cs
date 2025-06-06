using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LargeHammer : WeaponBase
{
    public GameObject objectPrefab;
    public float rotationAngle = 180f;
    public float rotationDuration = 1f;
    public Vector3 offset = new Vector3(0, 0, 0);
    public Transform realSpawnPos;
    public Transform spawnPt; //for sake of aiming rotaion

    private GameObject objectInstance;
    private float rotationTimer = 0f;
    
    private bool isRotating = false;
    private Vector3 initialPosition;

    //variables for dynamic scaling
    public float interval = -1f;
    public float tempNew;
    public bool newVal = false;
    public bool hammerActive;
    
    float hammerTime = 3;

    private bool isInGameScene;
    public AudioSource hammerSound;
    private bool hasPlayed = false;

    public override void ActivateThisWeapon()
    {
        //Debug.Log("Hammer activated");
        if(!hammerActive)
        {
            //Debug.Log("Hammer not active");
            if(interval >= 0)
            {
               // Debug.Log("Interval is greater or equal to 0");
                interval -= Time.deltaTime;
            }
            if (interval < 0)
            {
               // Debug.Log("Interval is less than 0");
                hammerSound.Play();
                objectInstance = Instantiate(objectPrefab, realSpawnPos.position, objectPrefab.transform.rotation);
                objectInstance.transform.SetParent(spawnPt, false);
                objectInstance.transform.Rotate(Vector3.forward, -90f);
                initialPosition = objectInstance.transform.position;
                rotationTimer = 0f;
                isRotating = true;
                if (!newVal)
                {
                    interval = 3.0f;
                }
                else
                {
                    interval = tempNew;
                }
            }

            hammerActive = true;
        }
        else
        {
            hammerTime -= 0.2f;
        }
    }

    void Start()
    {
        hammerSound = GetComponent<AudioSource>();
    }

    void Update()
    {
      /*  while (!hammerActive && interval >= 0)
        {
            interval -= Time.deltaTime;
        }*/
        if (isRotating)
        {
            RotateObject();
        }

        //gets scene name
        Scene currentScene = SceneManager.GetActiveScene();
        isInGameScene = currentScene.name == "GameScene";

        //If player is in the game scene
        if (isInGameScene == true && hasPlayed == false)
        {
            if (hammerSound != null)
           {
                hammerSound.Play();
                hasPlayed = true;
            }
        }

    }

    /// <summary>
    /// Rotate hammer when active to simulate hammer hitting enemies
    /// </summary>
    void RotateObject()
    {
        rotationTimer += Time.deltaTime;
        float angle = Mathf.Lerp(0, rotationAngle, rotationTimer / rotationDuration);
        objectInstance.transform.RotateAround(transform.position, Vector3.back, angle * Time.deltaTime);

        if (rotationTimer >= rotationDuration)
        {
            isRotating = false;
            //Once finished rotation, destroy
            Destroy(objectInstance);

            StartCoroutine(TimeReload());
            
        }
    } //END RotateObject()

    //Handle hammer level up
    public void LevelUp(int upgradeTier)
    {
        float newInterval;
        if (upgradeTier < 3)
        {
            newInterval = (0.5f * (float)upgradeTier);
        }
        else 
        {
            newInterval = (.25f * ((float)upgradeTier - 3));
        }
        SetInterval(newInterval);
    } 

    public void SetInterval(float newInterval)
    {
        newVal = true;
        tempNew = interval - newInterval;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //damage stuff
            //Debug.Log("Enemy hit");
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(5);
        }
    }


    IEnumerator TimeReload()
    {
        yield return new WaitForSeconds(hammerTime);
        Reload();
    }

    /// <summary>
    /// Instantiate new hammer after interval has passed
    /// </summary>
    void Reload()
    {
        objectInstance = Instantiate(objectPrefab, transform.position + offset, Quaternion.identity);
        objectInstance.transform.SetParent(spawnPt);
        objectInstance.transform.Rotate(Vector3.forward, -90f);
        initialPosition = objectInstance.transform.position;
        rotationTimer = 0f;
        isRotating = true;
    } //END Reload()
}
