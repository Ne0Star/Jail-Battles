using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] protected int batchCount;
    [SerializeField] private List<ShopItem> items = new List<ShopItem>();

    private void Awake()
    {

    }

    public T GetFreeItem<T>() where T : ShopItem
    {
        T result = null;
        foreach (ShopItem item in items)
        {
            if (item && item as T && !item.gameObject.activeSelf)
            {
                result = (T)item;
                break;
            }
        }
        return result;
    }

    public void AddItem(ShopItem item)
    {

    }

}
