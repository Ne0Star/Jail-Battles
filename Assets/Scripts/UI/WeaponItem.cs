using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ShopItems/Weapon")]
public class WeaponItem : ScriptableObject
{
    [SerializeField] private Sprite shopIco;
    [SerializeField] private Weapon m_Weapon;
    [SerializeField] private float m_Price;
    public Weapon Weapon { get => m_Weapon; }
    public float Price { get => m_Price; }
    public Sprite ShopIco { get => shopIco; }
}
