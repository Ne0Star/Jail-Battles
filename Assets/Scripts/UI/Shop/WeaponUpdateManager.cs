using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponUpdateManager : MonoBehaviour
{
    [SerializeField] private int batchCount = 20;

    [SerializeField] private Transform itemsParent;
    [SerializeField] private WeaponShopItemUpdate itemPrefab;
    private WeaponShopItemUpdate[] items;

    private void Awake()
    {
        items = new WeaponShopItemUpdate[batchCount];
        for (int i = 0; i < batchCount; i++)
        {
            WeaponShopItemUpdate item = Instantiate(itemPrefab, itemsParent);
            item.gameObject.SetActive(false);
            items[i] = item;
        }
    }


    public void SetItem(WeaponItem item, ref WeaponData data)
    {
        foreach (WeaponShopItemUpdate sp in items)
        {
            sp.gameObject.SetActive(false);
        }

        if (data.attackDistance.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref data.attackDistance, "attackRange");
        }
        if (data.reloadSpeed.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref data.reloadSpeed, "reloadSpeed");
        }
        if (data.attackSpeed.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref data.attackSpeed, "attackSpeed");
        }
        if (data.attackDamage.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref data.attackDamage, "attackDamage");
        }
        if (data.patronCount.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref data.patronCount, "patronCount");
        }
        if (data.attackCount.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref data.attackCount, "attackCount");
        }
        if (data.patronStorage.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref data.patronStorage, "patronStorage");
        }
        if (data.shootingAccuracy.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref data.shootingAccuracy, "accuracy");
        }
    }

    private WeaponShopItemUpdate GetFreeItem()
    {
        WeaponShopItemUpdate result = null;
        foreach (WeaponShopItemUpdate item in items)
        {
            if (!item.gameObject.activeSelf)
            {
                result = item;
                break;
            }
        }
        return result;
    }
}
