using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StoreMenuScript : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject notEnough;
    private void Start()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        SaveData _save = playerHealth.gameObject.GetComponent<SaveData>();
        _save.LoadPlayerData();
        ShowMoneyText();
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowMoneyText()
    {
        moneyText.text = "Owned: $" + playerHealth.money.ToString();
    }

    //Manage money UI showing if player can make purchases
    public void NotEnoughMoney()
    {
        notEnough.SetActive(true);
        StartCoroutine(DisableNotEnoughMoney());
    }

    IEnumerator DisableNotEnoughMoney()
    {
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("Coroutine for not enough disable");
        notEnough.SetActive(false);
    }
}
