using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollect : MonoBehaviour
{
    //Attach to coins, make collider much larger than coin to make it easier for player to get the coins

    private int secondsBeforeDespawn = 5;

    private void Start()
    {
        StartCoroutine(Despawn());
    }

    private void OnTriggerEnter(Collider other)
    {
        AddMoney(other);
    }

    void AddMoney(Collider _other)
    {
        if(_other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            PlayerHealth _player = _other.gameObject.GetComponent<PlayerHealth>();
            _player.money++;
            Destroy(this.gameObject);
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(secondsBeforeDespawn);
        Destroy(this.gameObject);
    }
}
