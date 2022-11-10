using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpdateManager : MonoBehaviour
{
    [SerializeField] private Text attackSpeed_v, reloadSpeed_v, attackCount_v, attackDamage_v;


    [SerializeField] private Transform attackSpeed_vT, reloadSpeed_vT, attackCount_vT, attackDamage_vT;

    [SerializeField] private WeaponShop shop;

    private void Awake()
    {
        shop.onSelected += SetItem;
    }

    private void SetItem(WeaponItem item)
    {
        attackCount_v.text = item.Weapon.AttackCount + " ";
        reloadSpeed_v.text = item.Weapon.ReloadSpeed + " ";
        attackSpeed_v.text = item.Weapon.AttackSpeed + " ";
        attackDamage_v.text = item.Weapon.AttackDamage + " ";


        Set(item, attackSpeed_vT, item.Weapon.AttackSpeed, item.Weapon.UpdateStats[item.Weapon.UpdateStats.Length - 1].AttackSpeed);

    }


    private void Set(WeaponItem item, Transform target, float value, float maxValue)
    {
        int childCount = target.childCount;
        float precent = value * 100 / maxValue;
        float coof = (childCount + 1) / 100f;
        int count = Mathf.RoundToInt(coof * precent);
        //Debug.Log("Процентов: " + precent + " Количество: " + count + " Максимальное количество: " + childCount + " Коофицент: " + coof);
        if (count == childCount) return;
        foreach (Transform t in target)
        {
            t.gameObject.SetActive(false);
        }

        for (int i = 0; i < childCount; i++)
        {
            if (i < count)
            {
                Transform t = target.GetChild(i).transform;
                t.gameObject.SetActive(true);
            }
        }
    }
}
