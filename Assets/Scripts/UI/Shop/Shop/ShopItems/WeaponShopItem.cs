using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopItem : ShopItem
{
    public event System.Action<WeaponItem> onByuAttemp;
    [SerializeField] private Button btn;
    [SerializeField] private WeaponItem item;
    [SerializeField] private Image ico;

    public WeaponItem Item { get => item; set => item = value; }

    private void Awake()
    {
        if (btn)
            btn.onClick?.AddListener(() => onByuAttemp?.Invoke(item));
    }

    public void SetItem(WeaponItem item)
    {
        this.item = item;
        UpdateData();
    }

    private void UpdateData()
    {
        if (item)
        {
            if (ico)
                ico.sprite = item.ShopIco;
        }
    }

    private void OnDisable()
    {
        onByuAttemp = null;
    }

    private void OnEnable()
    {
        UpdateData();
    }
}
