using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessLevels : MonoBehaviour
{
    //Made for Char - for UI in game scene for upgrade activation

    UpgradeScriptObj healthUpgrade;
    UpgradeScriptObj xpUpgrade;
    UpgradeScriptObj speedUpgrade;
    UpgradeScriptObj lifeForceUpgrade;
    UpgradeScriptObj forcefieldUpgrade;

    //To access the level of any of these upgrades:
    //Do the variable associated w the upgrade and do .level after it
    //eg the health upgrade's level is accessed by healthUpgrade.level
}
