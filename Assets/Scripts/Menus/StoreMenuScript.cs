using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StoreMenuScript : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private TextMeshProUGUI moneyText;
    private void Start()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();

        moneyText.text = "Owned: $" + playerHealth.money.ToString();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
