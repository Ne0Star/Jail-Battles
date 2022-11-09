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
    [SerializeField] private Text reloadSpeed_v, attackSpeed_v, attackCount_v, attackDamage_v;

    public WeaponItem Item { get => item; set => item = value; }

    private void Awake()
    {
        if (btn)
            btn.onClick?.AddListener(() => onByuAttemp?.Invoke(item));
    }

    public void SetVisibilityData(Text reloadSpeed_v, Text attackSpeed_v, Text attackCount_v, Text attackDamage_v, Image ico)
    {
        this.reloadSpeed_v = reloadSpeed_v;
        this.attackSpeed_v = attackSpeed_v;
        this.attackCount_v = attackCount_v;
        this.attackDamage_v = attackDamage_v;
        this.ico = ico;
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
            if (reloadSpeed_v)
                reloadSpeed_v.text = item.Weapon.ReloadSpeed + " ";
            if (attackSpeed_v)
                attackSpeed_v.text = item.Weapon.AttackSpeed + " ";
            if (attackCount_v)
                attackCount_v.text = item.Weapon.AttackCount + " ";
            if (attackDamage_v)
                attackDamage_v.text = item.Weapon.AttackDamage + " ";
            if (ico)
                ico.sprite = item.ShopIco;
        }
    }

    private void OnEnable()
    {
        UpdateData();
    }
}
