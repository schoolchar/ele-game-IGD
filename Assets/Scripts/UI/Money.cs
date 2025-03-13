using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Money : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI money;
    float coins = 0;
    // Update is called once per frame
    void Update()
    {
        coins = coins += 1;
        money.text = coins.ToString();
        DontDestroyOnLoad(money);
    }
}
