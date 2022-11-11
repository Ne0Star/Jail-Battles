using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public List<WeaponCategory> allowTypes;
    [SerializeField] private Image weaponIco;

    public bool SetWeaponItem(WeaponItem item)
    {
        bool result = false;
        foreach (WeaponCategory type in allowTypes)
        {
            if (type == item.Weapon.WeaponCategory)
            {
                weaponIco.sprite = item.ShopIco;
                result = true;
                break;
            }
        }
        return result;
    }

}
