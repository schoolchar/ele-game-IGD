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
        //For every frame enemies stay in the water, do damage
        if(other.gameObject.tag == "Enemy")
        {
            Attack(other.gameObject.GetComponent<EnemyHealth>()); 
        }
    }


    public void Attack(EnemyHealth _enemy)
    {
        
    }

    /// <summary>
    /// Expand water spout range
    /// </summary>
    private IEnumerator DelayWaterSpread(int _nextWaterIndx)
    {
        //After a half a second, enable the next water plane to create the illusion of water spreading in a spout
        yield return new WaitForSeconds(0.5f);
        waterPlanes[_nextWaterIndx].SetActive(true);

        if (_nextWaterIndx + 1 != waterPlanes.Length)
        {
            StartCoroutine(DelayWaterSpread(_nextWaterIndx + 1));
        }
    } //END DelayWaterSpread()

    /// <summary>
    /// Start attack
    /// </summary>
    private IEnumerator StartWaterSpout()
    {
        yield return new WaitForSeconds(timeBWAttack);
        waterPlanes[0].SetActive(true);
        StartCoroutine(EndWaterSpout());
        StartCoroutine(DelayWaterSpread(1));
    } //END StartWaterSpout()

    /// <summary>
    /// Stop Attack
    /// </summary>
    private IEnumerator EndWaterSpout()
    {
        yield return new WaitForSeconds(duration);

        for(int i = 0; i < waterPlanes.Length; i++)
        {
            waterPlanes[i].SetActive(false);
        }
        StopAllCoroutines();
        StartCoroutine(StartWaterSpout());
    } //END EndWaterSpout()
}
