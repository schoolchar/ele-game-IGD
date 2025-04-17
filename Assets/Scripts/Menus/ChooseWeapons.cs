using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseWeapons : MonoBehaviour
{
    public struct WeaponsData
    {
        public string name;
        public int level;
        public int index; //Index in allWeaponsData
        public GameObject inSceneObj;
    }


    private int oldXPNum;
    private int currentXP;
    private int nextMilestone = 5;
    [SerializeField] private Canvas weaponsCanvas;
    private PlayerMovement player;
    private PlayerHealth playerHealth;

    [SerializeField] private TextMeshProUGUI ringText;
    [SerializeField] private TextMeshProUGUI knifeText;

    public Dictionary<string, int> weaponData = new Dictionary<string, int>(); //store names and levels of all wepons
    [SerializeField] private Button[] weaponMenu; //Buttons on weapons menu
    [SerializeField] private TextMeshProUGUI[] weaponMenuText; //Corresponding text for each button
    public WeaponsData[] allWeaponsData; //Will need to assign indeces for each weapons
    public WeaponsData[] weaponOptions = new WeaponsData[3];
    int[] showWeapons = new int[3];


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
        RandomizeWeaponSelection();


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

    //Enable the ring of fire weapon
    public void EnableRingOfFire()
    {
        ringText.text = "Ring of Fire enabled";

        player.ringOfFire.SetActive(true);
        DeactivateMenu();
    }

    //Enable the knife throw weapon
    public void EnableKnifeThrow()
    {
        knifeText.text = "Knife Throw enabled";

        player.knifeThrow.enabled = true;
        player.knifeThrow.hasKnife = true;
        DeactivateMenu();
    }

    /// <summary>
    /// Randomize which weapons are assigned to the buttons on the weapon selection menu
    /// </summary>
    void RandomizeWeaponSelection()
    {
        //Chose random weapons to show
        

        for (int i = 0; i < 3; i++)
        {
            int _tmp = Random.Range(0, allWeaponsData.Length - 1);
            
            //Ensure that among the chosen ones, the same one does not show more than once
            for (int j = 0; j < showWeapons.Length; j++)
            {
                if (_tmp == showWeapons[j])
                {
                    i--;
                    break;

                }
                else
                {
                    showWeapons[i] = _tmp; //store the index that the wep
                    weaponOptions[i] = allWeaponsData[_tmp]; //store chosen weapon in options, this will correspond to the button that the weapon is at
                }
            }


        }

        //Input data onto buttons
        for (int i = 0; i < weaponMenuText.Length; i++)
        {
            weaponMenuText[i].text = weaponOptions[i].name + " " + weaponOptions[i].level;
            
        }
        ActivateWeapon();

    } //END RandomizeWeaponSelection()

    /// <summary>
    /// Attach respective enabling method to each button
    /// </summary>
    public void ActivateWeapon()
    {
        for(int i = 0; i < weaponOptions.Length; i++)
        {
            switch (weaponOptions[i].index)
            {
                case 0:
                    if (weaponOptions[i].level == 0)
                        weaponMenu[i].onClick.AddListener(this.EnableRingOfFire);
                    else
                    {
                        //Something for upgading the weapon
                    }
                    break;
                case 1:
                    if (weaponOptions[i].level == 0)
                        weaponMenu[i].onClick.AddListener(this.EnableRingOfFire);
                    else
                    {
                        //Something for upgading the weapon
                    }
                    break;
               // case 2:
               // case 3:
            }
        }
       
    } //END ActivateWeapon()
}
