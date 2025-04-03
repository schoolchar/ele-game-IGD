using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseWeapons : MonoBehaviour
{
    private int oldXPNum;
    private int currentXP;
    private int nextMilestone = 5;
    [SerializeField] private Canvas weaponsCanvas;
    private PlayerMovement player;
    private PlayerHealth playerHealth;

    [SerializeField] private TextMeshProUGUI ringText;
    [SerializeField] private TextMeshProUGUI knifeText;


    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        playerHealth = player.gameObject.GetComponent<PlayerHealth>();
    }

    /// <summary>
    /// Enables weapon choosing menu, right now uses intervals mult 2
    /// </summary>
    public void ActivateMenu(int _currentXP)
    {
        //Check that xp is at next milestone, prevents running every time that xp is gained
        if(_currentXP >= nextMilestone && _currentXP != oldXPNum)
        {
            Time.timeScale = 0;
            weaponsCanvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerHealth.level++;
        }
    }
    /// <summary>
    /// Deactivates menu after choosing
    /// </summary>
    private void DeactivateMenu()
    {
        Time.timeScale = 1;
        oldXPNum = currentXP;
        nextMilestone *= 2;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        weaponsCanvas.enabled = false;
    }

    public void EnableRingOfFire()
    {
        ringText.text = "Ring of Fire enabled";

        player.ringOfFire.SetActive(true);
        DeactivateMenu();
    }

    public void EnableKnifeThrow()
    {
        knifeText.text = "Knife Throw enabled";

        player.knifeThrow.enabled = true;
        player.knifeThrow.hasKnife = true;
        DeactivateMenu();
    }
}
