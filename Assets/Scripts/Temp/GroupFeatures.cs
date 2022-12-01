using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "�������", menuName = "�������")]
public class GroupFeatures : ScriptableObject
{

    public enum GroupSize
    {
        ��������� = 10,
        ������� = 25,
        ������� = 50
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
