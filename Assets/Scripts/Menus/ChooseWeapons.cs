using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseWeapons : MonoBehaviour
{
    [System.Serializable]
    public struct WeaponsData
    {
        public string name;
        public int level;
        public int index; //Index in allWeaponsData
        public GameObject inSceneObj;
        public WeaponBase script;
    }

    //Ring of fire - 0
    //Knife Throw - 1
    //Hammer - 2
    //Turret - 3


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
    public int[] weaponOptions = new int[3] { -1, -1, -1}; //The ideces for each button (ie button 1 is 0, button 2 is 1, etc).  What is stored in each is the index that the assigned weapon is in in allWeaponsData
    [SerializeField] List<int> availableOps = new List<int>(); //Needs to be the same size as allWeaponsData




    private void Start()
    {
        //availableOps = new List<int>(3);
        
         for(int i = 0; i < allWeaponsData.Length; i++)
         {
             availableOps.Add(i);
         }

        Debug.Log("avalable opstopns = " + availableOps.Count);

        player = FindAnyObjectByType<PlayerMovement>();
        playerHealth = player.gameObject.GetComponent<PlayerHealth>();


        //Set objects in script
        //Ring of fire
        allWeaponsData[0].script = FindAnyObjectByType<Ringoffire>();
        allWeaponsData[0].inSceneObj = allWeaponsData[0].script.gameObject;
        allWeaponsData[0].inSceneObj.SetActive(false);

        allWeaponsData[1].inSceneObj = FindAnyObjectByType<Knifethrow>().knife;
        allWeaponsData[1].script = FindAnyObjectByType<Knifethrow>();
        allWeaponsData[1].script.enabled = false;
        allWeaponsData[1].inSceneObj.SetActive(false);

        allWeaponsData[2].script = FindAnyObjectByType<LargeHammer>();
        allWeaponsData[2].script = allWeaponsData[2].script;
        allWeaponsData[2].script.enabled = false;
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
        

        for(int i = 0; i < weaponOptions.Length; i++)
        {
            weaponOptions[i] = -1;
        }
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
        

        for (int i = 0; i < weaponMenu.Length; i++)
        {
            int _rTmp = Random.Range(0, availableOps.Count - 1);
            Debug.Log("Choose at index " + _rTmp);
            int _tmp = availableOps[_rTmp];
            availableOps.RemoveAt(_rTmp);
            weaponOptions[i] = _tmp; //store chosen weapon in options, this will correspond to the button that the weapon is at

            //Ensure that among the chosen ones, the same one does not show more than once
           /* for (int j = 0; j < availableOps.Count; j++)
            {
                if (_tmp == weaponOptions[i])
                {
                   
                    i--;
                    break;

                }
               
                weaponOptions[i] = _tmp; //store chosen weapon in options, this will correspond to the button that the weapon is at
                
            }*/


        }

        

        //Input data onto buttons
        for (int i = 0; i < weaponMenuText.Length; i++)
        {
            weaponMenuText[i].text = allWeaponsData[weaponOptions[i]].name + " " + allWeaponsData[weaponOptions[i]].level;
            
        }


        //Re-initialize the available options after choosing

        availableOps.Clear();
        for (int i = 0; i < allWeaponsData.Length; i++)
        {
            availableOps.Add(i);
        }


        //ActivateWeapon();

    } //END RandomizeWeaponSelection()

    /// <summary>
    /// Attach respective enabling method to each button
    /// </summary>
    public void ActivateWeapon(int _idx)
    {
        /* for(int i = 0; i < weaponOptions.Length; i++)
         {
             switch (allWeaponsData[weaponOptions[i]].index)
             {
                 case 0:
                     if (allWeaponsData[weaponOptions[i]].level == 0)
                         weaponMenu[i].onClick.AddListener(this.EnableRingOfFire);
                     else
                     {
                         //Something for upgading the weapon
                     }
                     break;
                 case 1:
                     if (allWeaponsData[weaponOptions[i]].level == 0)
                         weaponMenu[i].onClick.AddListener(this.EnableRingOfFire);
                     else
                     {
                         //Something for upgading the weapon
                     }
                     break;
                // case 2:
                // case 3:
             }
         }*/

        if(allWeaponsData[weaponOptions[_idx]].inSceneObj != null)
        {
            allWeaponsData[weaponOptions[_idx]].inSceneObj.SetActive(true);
        }
        
        allWeaponsData[weaponOptions[_idx]].script.enabled = true;
        allWeaponsData[weaponOptions[_idx]].script.ActivateThisWeapon();
        allWeaponsData[weaponOptions[_idx]].level++;

        DeactivateMenu();


    } //END ActivateWeapon()
}
