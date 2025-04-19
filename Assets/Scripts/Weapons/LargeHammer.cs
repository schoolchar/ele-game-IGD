using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LargeHammer : WeaponBase
{
    public GameObject objectPrefab;
    public float rotationAngle = 180f;
    public float rotationDuration = 1f;
    public Vector3 offset = new Vector3(1, 0, 0);
    public Transform spawnPt; //for sake of aiming rotaion

    private GameObject objectInstance;
    private float rotationTimer = 0f;
    
    private bool isRotating = false;
    private Vector3 initialPosition;

    //variables for dynamic scaling
    public float interval = 3.0f;
    public float tempNew;
    public bool newVal = false;


    bool hammerActive;
    public override void ActivateThisWeapon()
    {
        Debug.Log("Hammer activated");
        if(!hammerActive)
        {
            interval -= Time.deltaTime;
            if (interval < 0)
            {
                objectInstance = Instantiate(objectPrefab, transform.position + offset, Quaternion.identity);
                objectInstance.transform.SetParent(spawnPt);
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
            Debug.Log("Hammer upgrade what is the scaling for this weapon");
        }
       
        
    }

    void Update()
    {
        if (isRotating)
        {
            RotateObject();
        }
        
    }

    void RotateObject()
    {
        rotationTimer += Time.deltaTime;
        float angle = Mathf.Lerp(0, rotationAngle, rotationTimer / rotationDuration);
        objectInstance.transform.RotateAround(transform.position, Vector3.up, angle * Time.deltaTime);

        if (rotationTimer >= rotationDuration)
        {
            isRotating = false;
            Destroy(objectInstance);

            StartCoroutine(TimeReload());
            
        }
    }

    public void SetInterval(float newInterval)
    {
        newVal = true;
        interval = newInterval;
        tempNew = newInterval;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //damage stuff
            Debug.Log("Enemy hit");
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
        }
    }


    IEnumerator TimeReload()
    {
        yield return new WaitForSeconds(3);
        Reload();
    }

    void Reload()
    {
        objectInstance = Instantiate(objectPrefab, transform.position + offset, Quaternion.identity);
        objectInstance.transform.SetParent(spawnPt);
        objectInstance.transform.Rotate(Vector3.forward, -90f);
        initialPosition = objectInstance.transform.position;
        rotationTimer = 0f;
        isRotating = true;
    }
}