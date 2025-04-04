using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDrop : MonoBehaviour
{
    //Attach to enemy

    [SerializeField] private int money;
    [SerializeField] private GameObject moneyObj;
    [SerializeField] private GameObject[] dropPos;

    /// <summary>
    /// When enemy dies, drop money
    /// </summary>
    public void DropCoins() //Should be called on enemy death
    {
        Debug.Log("Drop coins");
        int _posInx = 0;

        //Instantiate moeny for every coin enemy has
        for(int i = 0; i < money; i++)
        {
            Instantiate(moneyObj, dropPos[_posInx].transform.position, moneyObj.transform.rotation);
            _posInx++;
            if(_posInx >= dropPos.Length)
            {
                _posInx = 0;
            }
        }

        Destroy(this.gameObject);
    } //End DropCoins()
}
