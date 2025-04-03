using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoldMagnetismAndArmor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI magLeveltxt;
    [SerializeField] private TextMeshProUGUI armorLeveltxt;


    private void Start()
    {
        if(FindAnyObjectByType<Magnetism>() != null)
        {
            Magnetism _mag = FindAnyObjectByType<Magnetism>();
            magLeveltxt.text = "Level: " + _mag.scriptObj.level.ToString();
        }
        else
        {
            magLeveltxt.text = "Level: " + 0.ToString();
        }


            armorLeveltxt.text = "Level: " + 0.ToString();
    }

    public void ActivateMagnetism()
    {
        Magnetism _mag = FindAnyObjectByType<Magnetism>();
        _mag.ActivateUpgrade();
        magLeveltxt.text = "Level: " + _mag.scriptObj.level.ToString();
    }

    public void ActivateArmor()
    {
        //Hold functionality for turning on armor
    }
}
