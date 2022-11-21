using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "WeaponStat", menuName = "WeaponStat")]
public class WeaponStat : ScriptableObject
{
    public bool hideEnemiesData = false;
    public bool hidePlayerData = false;

    public List<bool> hidenPlayerStat;
    public List<bool> hidenEnemiesStat;

    public List<WeaponData> playerWeapons;
    public List<WeaponData> enemuWeapons;
}
