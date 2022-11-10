using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : Shop
{
    [SerializeField] private Transform listParent;
    [SerializeField] private WeaponShopItem itemPrefab;
    [SerializeField] private WeaponItem selectedItem;
    [SerializeField] private List<WeaponItem> shopStorage = new List<WeaponItem>();
    [SerializeField] private List<WeaponShopItem> allItems = new List<WeaponShopItem>();

    private void Awake()
    {
        for (int i = 0; i < batchCount; i++)
        {
            CreateItem();
        }

    }

    private void Byu(WeaponItem item)
    {
        Debug.Log("Попытка купить" + item.name);
    }

    private void SetPreviewItem(WeaponItem item)
    {

    }

    public void SetCategory(int categoryIndex)
    {
        RemoveAllItem();
        WeaponCategory category = (WeaponCategory)categoryIndex;
        foreach (WeaponItem item in shopStorage)
        {
            if (item.Weapon.WeaponCategory == category)
            {
                AddItem(item);
            }
        }
    }

    private void AddItem(WeaponItem itemType)
    {
        WeaponShopItem item = GetFreeItem();
        item.SetItem(itemType);
        item.onByuAttemp += SetPreviewItem;
        item.gameObject.SetActive(true);
    }

    public void RemoveAllItem()
    {
        foreach (WeaponShopItem item in allItems)
        {
            if (!item.gameObject.activeSelf) return;
            item.gameObject.SetActive(false);
        }
    }

    private WeaponShopItem GetFreeItem()
    {
        foreach (WeaponShopItem item in allItems)
        {
            if (!item.gameObject.activeSelf)
                return item;
        }
        return null;
    }

    private WeaponShopItem CreateItem()
    {
        WeaponShopItem item = null;
        if (itemPrefab)
        {
            item = Instantiate(itemPrefab, listParent);
            item.gameObject.SetActive(false);
            allItems.Add(item);
        }
        else
        {
            GameObject go = new GameObject();
            go.transform.parent = listParent;
            go.SetActive(false);
            item = go.AddComponent<WeaponShopItem>();
            allItems.Add(item);
        }

        return item;
    }

    private void Start()
    {

    }

}
