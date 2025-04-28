using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarryOver : MonoBehaviour
{
    [System.Serializable]
    public struct ToSaveUpgrades
    {
        public bool upgradeActivated;
        public int upgradeLevel;
        public int modVal;
    }

    [System.Serializable]
    public struct ToSaveAnimals
    {
        bool hasBeenBought;
        public string thisAnimal;
        public bool isActiveAnimal;
    }
    
    public ToSaveUpgrades[] savedUpgrades; 
    public ToSaveAnimals[] savedAnimals;
    public bool changedOnThis; //Keeps track of if anything was changed on this object vs the player

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject); //Don't destroy until a player exists in the game, then pass over data and destroy
    }

    public void DestroyThis()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1 && !changedOnThis)
        {
            Destroy(this.gameObject);
        }
    }


}
