using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : MonoBehaviour, ICharacterActions
{
    //Elephant has buckets of water on its sides, sprays water (long range attack, slowly expands in range);

    [SerializeField] private GameObject[] waterPlanes;
    private float duration = 3;
    private float timeBWAttack = 4;


    void Start()
    {
        StartCoroutine(StartWaterSpout());
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Attack(other.gameObject.GetComponent<EnemyHealth>()); 
        }
    }


    public void Attack(EnemyHealth _enemy)
    {
        
    }

    private IEnumerator DelayWaterSpread(int _nextWaterIndx)
    {
        yield return new WaitForSeconds(0.5f);
        waterPlanes[_nextWaterIndx].SetActive(true);

        if (_nextWaterIndx + 1 != waterPlanes.Length)
        {
            StartCoroutine(DelayWaterSpread(_nextWaterIndx + 1));
        }
    }

    private IEnumerator StartWaterSpout()
    {
        yield return new WaitForSeconds(timeBWAttack);
        waterPlanes[0].SetActive(true);
        StartCoroutine(EndWaterSpout());
        StartCoroutine(DelayWaterSpread(1));
    }

    private IEnumerator EndWaterSpout()
    {
        yield return new WaitForSeconds(duration);

        for(int i = 0; i < waterPlanes.Length; i++)
        {
            waterPlanes[i].SetActive(false);
        }
        StopAllCoroutines();
        StartCoroutine(StartWaterSpout());
    }
}
