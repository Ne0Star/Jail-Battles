using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class WeaponShopItem : ShopItem
{
    public event System.Action<WeaponItem> onSelect;
    [SerializeField] private Button btn;
    [SerializeField] private WeaponItem item;
    [SerializeField] private Image ico;

    public WeaponItem Item { get => item; set => item = value; }

    private void Awake()
    {
        if (btn)
            btn.onClick?.AddListener(() => onSelect?.Invoke(item));
    }

    public void SetItem(WeaponItem item)
    {
        this.item = item;
        UpdateData();
    }

    private void UpdateData()
    {
        if(item)
            if (ico)
                ico.sprite = item.ShopIco;
    }

    private void OnDisable()
    {
        onSelect = null;
    }

    private void OnEnable()
    {
        UpdateData();
    }
}
