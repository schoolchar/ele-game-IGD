using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollect : MonoBehaviour
{
    //Attach to coins, make collider much larger than coin to make it easier for player to get the coins

    private int secondsBeforeDespawn = 5;
    private SaveData saveData;

    private void Start()
    {
        //get save data and start coroutine to despawn on timer
        StartCoroutine(Despawn());
        saveData = FindAnyObjectByType<SaveData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AddMoney(other);
    }

    //if player collides w/ money, then increase moeny the player has
    void AddMoney(Collider _other)
    {
        if(_other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            PlayerHealth _player = _other.gameObject.GetComponent<PlayerHealth>();
            //Increase money
            _player.money++;
            //Save money player has
            saveData.SaveMoney();
            //Destroy object
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Can not collect");
        }
    }

    //Money despawns if it is not collected in time
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(secondsBeforeDespawn);
        Destroy(this.gameObject);
    }
}
