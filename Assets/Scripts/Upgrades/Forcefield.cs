using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Forcefield : UpgradeParent
{ //Not a scalable upgrade right now

    ForcefieldOnPlayer forceFieldScript;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        forceFieldScript = FindAnyObjectByType<ForcefieldOnPlayer>();
    }

    public override void ActivateUpgrade()
    {
        forceFieldScript.ActivateForcefield();
    }

    public override void IncreaseLevel()
    {
        base.IncreaseLevel();

        //Change text on screen
        levelText.text = scriptObj.level.ToString();
        saveData.SaveForcefieldUpgrade();
    }
}
