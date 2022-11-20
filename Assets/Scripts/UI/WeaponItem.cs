using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ShopItems/Weapon")]
public class WeaponItem : ScriptableObject
{
    [SerializeField] private bool useTranslator;
    [SerializeField] private string nameKey;

    [SerializeField] private Sprite shopIco;
    [SerializeField] private Weapon m_Weapon;
    [SerializeField] private int maxWeaponCount;
    [SerializeField] private int m_Price;
    [SerializeField] private int m_UpdatePrice;
    public Weapon Weapon { get => m_Weapon; }
    public int Price { get => m_Price; }
    public Sprite ShopIco { get => shopIco; }
    public int MaxWeaponCount { get => maxWeaponCount;}
    public bool UseTranslator { get => useTranslator; }
    public string NameKey { get => nameKey; }
    public int UpdatePrice { get => m_UpdatePrice; }
}
