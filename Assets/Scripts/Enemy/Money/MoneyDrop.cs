using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDrop : MonoBehaviour
{
    //Attach to enemy

    [SerializeField] private int money;
    [SerializeField] private GameObject moneyObj;
    [SerializeField] private GameObject[] dropPos;
    public void DropCoins() //Should be called on enemy death
    {
        int _posInx = 0;
        for(int i = 0; i < money; i++)
        {
            Instantiate(moneyObj, dropPos[_posInx].transform.position, moneyObj.transform.rotation);
            _posInx++;
            if(_posInx >= dropPos.Length)
            {
                _posInx = 0;
            }
        }
    }
}
