using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class ScinningShopItem : ShopItem
{
    [SerializeField] string category;
    [SerializeField] string label;
    [SerializeField] private PlayerView playerView;

    public event System.Action<string, string> onSelect;
    [SerializeField] private Button btn;
    [SerializeField] private WeaponItem item;
    [SerializeField] private Image ico;


    private void Awake()
    {
        if (btn)
            btn.onClick?.AddListener(() => onSelect?.Invoke(category,label));
    }


    public void SetItem(string category, string label, Sprite ico)
    {
        this.ico.sprite = ico;
        this.category = category;
        this.label = label;
    }

}
