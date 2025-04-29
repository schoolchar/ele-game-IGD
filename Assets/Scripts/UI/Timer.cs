using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timertext;
    float timerVal = 0;
    // Update is called once per frame
    void Update()
    {
        timerVal = timerVal + Time.deltaTime;
        timertext.text = timerVal.ToString();
    }

    /// <summary>
    /// For results page, check how long the player lasted in round
    /// </summary>
    public float PassTimeOnDeath()
    {
        return timerVal;
    } //END PassTimeOnDeath()
}
