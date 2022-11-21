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
    public Weapon Weapon { get => m_Weapon; }
    public Sprite ShopIco { get => shopIco; }
    public bool UseTranslator { get => useTranslator; }
    public string NameKey { get => nameKey; }
}
