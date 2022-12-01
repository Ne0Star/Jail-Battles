using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "������", menuName = "������ ��� ��������")]
public class WeaponShopStat : ScriptableObject
{
    public bool hidePlayerData = false;

    public List<bool> hidenPlayerStat;

    public Data data;

    [System.Serializable]
    public struct Data
    {
        public List<WeaponData> datas;
    }
}
