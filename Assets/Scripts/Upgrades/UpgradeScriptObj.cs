using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeScriptObj : ScriptableObject
{
    public int affectOnHealth;
    public int affectOnXP;
    public int affectOnSpeed;
    public int affectOnMag;
    public int magSpeed;
    public int deactivateTimeFF;
    public int level;
    public int cost;
}
