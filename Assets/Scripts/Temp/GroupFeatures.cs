using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Фракция", menuName = "Фракция")]
public class GroupFeatures : ScriptableObject
{

    public enum GroupSize
    {
        Маленькая = 10,
        Средняя = 25,
        Большая = 50
    }

    public WeaponStat weaponStat;


    public float speedBuff;
    public float damageBuff;
    public float healthBuff;

    public Color groupColor;


    public GroupSize groupSize;

    public string groupName;
    public string groupDescription;

    public string nameKey;
    public string descriptionKey;
}
