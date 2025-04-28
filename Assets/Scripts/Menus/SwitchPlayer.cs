using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class SwitchPlayer : MonoBehaviour
{
    public Elephant elephantComp;
    public Lion lionComp;
    public SeaLion seaLionComp;
    public Monkey monkeyComp;
    public BasicShoot baseComp;

    [SerializeField] private TextMeshProUGUI elephantCostTxt;
    [SerializeField] private TextMeshProUGUI monkeyCostTxt;
    [SerializeField] private TextMeshProUGUI sealCostTxt;


    public CharacterID[] playerModels;
    public GameObject characterActive;

    SaveData saveData;
    PlayerHealth playerHealth;
    [SerializeField] StoreMenuScript storeMenu;
    [SerializeField] private CarryOver carryOverData;

    public Color[] playerColors;

    //Animal costs, lion is default/free
    public int elephantCost = 30;
    public int monkeyCost = 50;
    public int sealCost = 80;

    int[] access = new int[5];

    private void Start()
    {
        //Get the types of player characters' scripts on the player
        elephantComp = FindAnyObjectByType<Elephant>();
        lionComp = FindAnyObjectByType<Lion>();
        baseComp = FindAnyObjectByType<BasicShoot>();
        seaLionComp = FindAnyObjectByType<SeaLion>();
        monkeyComp = FindAnyObjectByType<Monkey>();

        playerModels = FindObjectsByType<CharacterID>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        carryOverData = FindAnyObjectByType<CarryOver>();

        for (int i = 0; i < playerModels.Length; i++)
        {
            if (playerModels[i].animal == "Lion") //Temporary, save and load from txt file
            {
               characterActive = playerModels[i].gameObject;
            }
        }

        saveData = FindAnyObjectByType<SaveData>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();


        //Load what animals are owned and which are not
        string _path = Application.persistentDataPath + "/Animals.txt";
        
        if (File.Exists(_path))
        {
            string[] tmp = File.ReadAllLines(_path);
            for(int i = 0; i < tmp.Length; i++)
            {
                access[i] = int.Parse(tmp[i]);
            }
        }

        //Change text showing cost of each animal that is already owned
        if (access[1] == 1)
        {
            elephantCostTxt.text = "Owned";
        }
        if (access[2] == 1)
        {
            monkeyCostTxt.text = "Owned";
        }
        if (access[3] == 1)
        {
            sealCostTxt.text = "Owned";
        }


        
        
    }

    //Player chooses Lion character
    public void ActivateLion()
    {
        if(lionComp != null)
        {
            elephantComp.StopAllCoroutines();
            monkeyComp.StopAllCoroutines();
            seaLionComp.StopAllCoroutines();



            //Enable lion, disable everything else
            lionComp.enabled = true;
            elephantComp.enabled = false;
            baseComp.enabled = false;
            monkeyComp.enabled = false;
            seaLionComp.enabled = false;

            characterActive.SetActive(false);
            for (int i = 0; i < playerModels.Length; i++)
            {
                if (playerModels[i].animal == "Lion")
                {
                    playerModels[i].gameObject.SetActive(true);
                    characterActive = playerModels[i].gameObject;
                }
                else
                {
                    playerModels[i].gameObject.SetActive(false);
                }
            }

            if (saveData != null)
            {
                saveData.SaveUnlockedAnimals(0);
            }

        }
        else
        {

        }

        //playerMesh.material.color = playerColors[0];
    }

    //Player chooses elephant character
    public void ActivateElephant()
    {
       
        if (playerHealth.money >= elephantCost || access[1] == 1)
        {

            seaLionComp.StopAllCoroutines();
            lionComp.StopAllCoroutines();
            seaLionComp.StopAllCoroutines();


            //Enable elephant. disable everything else
            lionComp.enabled = false;
            elephantComp.enabled = true;
            seaLionComp.enabled = false;
            monkeyComp.enabled = false;
            baseComp.enabled = false;

            characterActive.SetActive(false);
            for (int i = 0; i < playerModels.Length; i++)
            {
                if (playerModels[i].animal == "Elephant")
                {
                    playerModels[i].gameObject.SetActive(true);
                    characterActive = playerModels[i].gameObject;
                }
                else
                {
                    playerModels[i].gameObject.SetActive(false);
                }
            }

            if (access[1] == 0)
            {
                playerHealth.money -= elephantCost;
                storeMenu.ShowMoneyText();
                elephantCostTxt.text = "Owned";
                access[1] = 1;

            }

            if (saveData != null)
            {
                saveData.SaveUnlockedAnimals(1);
            }


        }
        else
        {
            storeMenu.NotEnoughMoney();
        }


    }

    public void ActivateSeal()
    {
       
        if (playerHealth.money >= sealCost || access[3] == 1)
        {
            elephantComp.StopAllCoroutines();
            lionComp.StopAllCoroutines();
            monkeyComp.StopAllCoroutines();



            seaLionComp.enabled = true;
            lionComp.enabled = false;
            monkeyComp.enabled = false;
            baseComp.enabled = false;
            elephantComp.enabled = false;

            characterActive.SetActive(false);
            for (int i = 0; i < playerModels.Length; i++)
            {
                if (playerModels[i].animal == "Seal")
                {
                    playerModels[i].gameObject.SetActive(true);
                    characterActive = playerModels[i].gameObject;
                }
                else
                {
                    playerModels[i].gameObject.SetActive(false);
                }
            }
            if (access[3] == 0)
            {
                playerHealth.money -= sealCost;
                storeMenu.ShowMoneyText();
                sealCostTxt.text = "Owned";
                access[3] = 1;

            }

            if (saveData != null)
            {
                saveData.SaveUnlockedAnimals(3);
            }
        }
        else
        {
            storeMenu.NotEnoughMoney();
        }

    }

    public void ActivateMonkey()
    {
         

        if (playerHealth.money >= monkeyCost || access[2] == 1)
        {
            elephantComp.StopAllCoroutines();
            lionComp.StopAllCoroutines();
            seaLionComp.StopAllCoroutines();
            


            monkeyComp.enabled = true;
            seaLionComp.enabled = false;
            lionComp.enabled = false;
            baseComp.enabled = false;
            elephantComp.enabled = false;

            characterActive.SetActive(false);
            for (int i = 0; i < playerModels.Length; i++)
            {
                if (playerModels[i].animal == "Monkey")
                {
                    playerModels[i].gameObject.SetActive(true);
                    characterActive = playerModels[i].gameObject;
                }
                else
                {
                    playerModels[i].gameObject.SetActive(false);
                }
            }

            if (access[2] == 0)
            {
                playerHealth.money -= monkeyCost;
                storeMenu.ShowMoneyText();
                monkeyCostTxt.text = "Owned";
                access[2] = 1;

            }

            if (saveData != null)
            {
                saveData.SaveUnlockedAnimals(2);
            }

        }
        else
        {
            storeMenu.NotEnoughMoney();
        }
      
    }

    

}
