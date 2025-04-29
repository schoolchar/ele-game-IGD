using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultsScreen : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] private Timer timeVal;
    [SerializeField] private GameObject resultsObj;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI xpText;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        //PlayerHealth.onPlayerDeath += CALLBACK_ShowResults;
    }

    /// <summary>
    /// Set data for results page to show
    /// </summary>
    public void ShowResults()
    {
        //Debug.Log("Result obj = " + resultsObj.name);
         resultsObj.SetActive(true);
        //resultsObj.enabled = true;

        //Unlcok cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Load results from round
        timerText.text = "Show Duration: " + timeVal.PassTimeOnDeath().ToString();
        xpText.text = "XP Collected: " + playerHealth.xp.ToString();

        Time.timeScale = 0f;

    } //END ShowResults()

    //Load main menu on button press on screen
    public void MainMenuButton()
    {
        playerHealth.xp = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
