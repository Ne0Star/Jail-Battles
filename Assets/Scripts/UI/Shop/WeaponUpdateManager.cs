using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponUpdateManager : MonoBehaviour
{
    [SerializeField] private int batchCount = 20;

    [SerializeField] private Transform itemsParent;
    [SerializeField] private WeaponShopItemUpdate itemPab;
    private WeaponShopItemUpdate[] items;

    private void Awake()
    {
        items = new WeaponShopItemUpdate[batchCount];
        for (int i = 0; i < batchCount; i++)
        {
            WeaponShopItemUpdate item = Instantiate(itemPab, itemsParent);
            item.updateClick += ItemUpdated;
            item.gameObject.SetActive(false);
            items[i] = item;
        }
    }

    private WeaponData weaponDatas;


    private void ItemUpdated(WeaponShopItemUpdate item)
    {

    }

    public void SetItem(WeaponItem item, WeaponData data, ref WeaponData realData)
    {
        foreach (WeaponShopItemUpdate sp in items)
        {
            sp.gameObject.SetActive(false);
        }
        if (data.attackDistance.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref realData, ref realData.attackDistance, "attackRange");
        }
        if (data.reloadSpeed.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref realData, ref realData.reloadSpeed, "reloadSpeed");
        }
        if (data.attackSpeed.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref realData, ref realData.attackSpeed, "attackSpeed");
        }
        if (data.attackDamage.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref realData, ref realData.attackDamage, "attackDamage");
        }
        if (data.patronCount.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref realData, ref realData.patronCount, "patronCount");
        }
        if (data.attackCount.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref realData, ref realData.attackCount, "attackCount");
        }
        if (data.patronStorage.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref realData, ref realData.patronStorage, "patronStorage");
        }
        if (data.shootingAccuracy.AllowUpdate)
        {
            WeaponShopItemUpdate shopItem = GetFreeItem();
            shopItem.gameObject.SetActive(true);
            shopItem.SetStat(item, data, ref realData, ref realData.shootingAccuracy, "accuracy");
        }








        //foreach (WeaponShopItemUpdate sp in items)
        //{
        //    sp.gameObject.SetActive(false);
        //}
        //if (data.attackDistance.AllowUpdate)
        //{
        //    WeaponShopItemUpdate shopItem = GetFreeItem();
        //    shopItem.gameObject.SetActive(true);
        //    shopItem.SetStat(item, data, ref realData, ref realData.attackDistance, "attackRange");
        //}
        //if (data.reloadSpeed.AllowUpdate)
        //{
        //    WeaponShopItemUpdate shopItem = GetFreeItem();
        //    shopItem.gameObject.SetActive(true);
        //    shopItem.SetStat(item, data, ref realData, ref realData.reloadSpeed, "reloadSpeed");
        //}
        //if (data.attackSpeed.AllowUpdate)
        //{
        //    WeaponShopItemUpdate shopItem = GetFreeItem();
        //    shopItem.gameObject.SetActive(true);
        //    shopItem.SetStat(item, data, ref realData, ref realData.attackSpeed, "attackSpeed");
        //}
        //if (data.attackDamage.AllowUpdate)
        //{
        //    WeaponShopItemUpdate shopItem = GetFreeItem();
        //    shopItem.gameObject.SetActive(true);
        //    shopItem.SetStat(item, data, ref realData, ref realData.attackDamage, "attackDamage");
        //}
        //if (data.patronCount.AllowUpdate)
        //{
        //    WeaponShopItemUpdate shopItem = GetFreeItem();
        //    shopItem.gameObject.SetActive(true);
        //    shopItem.SetStat(item, data, ref realData, ref realData.patronCount, "patronCount");
        //}
        //if (data.attackCount.AllowUpdate)
        //{
        //    WeaponShopItemUpdate shopItem = GetFreeItem();
        //    shopItem.gameObject.SetActive(true);
        //    shopItem.SetStat(item, data, ref realData, ref realData.attackCount, "attackCount");
        //}
        //if (data.patronStorage.AllowUpdate)
        //{
        //    WeaponShopItemUpdate shopItem = GetFreeItem();
        //    shopItem.gameObject.SetActive(true);
        //    shopItem.SetStat(item, data, ref realData, ref realData.patronStorage, "patronStorage");
        //}
        //if (data.shootingAccuracy.AllowUpdate)
        //{
        //    WeaponShopItemUpdate shopItem = GetFreeItem();
        //    shopItem.gameObject.SetActive(true);
        //    shopItem.SetStat(item, data, ref realData, ref realData.shootingAccuracy, "accuracy");
        //}
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
