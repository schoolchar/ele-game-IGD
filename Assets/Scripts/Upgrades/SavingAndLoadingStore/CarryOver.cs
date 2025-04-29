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
    public ToSaveAnimals[] savedAnimals; //Same indexes as everywhere else
    public bool changedOnThis; //Keeps track of if anything was changed on this object vs the player

    //Upgrade indexes
    //Health - 0
    //XP - 1
    //Speed - 2
    //Life force - 3
    //Forcefield - 4

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject); //Don't destroy until a player exists in the game, then pass over data and destroy
    }

    //After passing off any information to the player instance, destroy this object
    public void DestroyThis()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1 && !changedOnThis)
        {
            Destroy(this.gameObject);
        }
    } //END DestroyThis()

    /// <summary>
    /// If player chooses an animal when player is not in the scene, save data
    /// </summary>
    public void ChangeAnimalState(int _index)
    {
        changedOnThis = true;
        //savedAnimals[_index].
    } //END ChangeAnimalState()

    public void ChangeUpgradeState(int _index)
    {

    }


}
