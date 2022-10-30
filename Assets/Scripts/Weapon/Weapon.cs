using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Тип оружия
/// </summary>
public enum WeaponType
{
    /// <summary>
    /// Нету оружия
    /// </summary>
    None,
    /// <summary>
    /// Пистолет
    /// </summary>
    Gun,
    /// <summary>
    /// Автомат
    /// </summary>
    Machine,
    /// <summary>
    /// Дубинка/короткая палка
    /// </summary>
    Mele,
}

/// <summary>
/// Оружие
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackDistance;
    [SerializeField] private float reloadSpeed;

    private Transform parent;

    [SerializeField] private WeaponType weaponType;

    public WeaponType WeaponType { get => weaponType; }

    private void Start()
    {
        parent = transform.parent;
    }

    private void OnDisable()
    {
        transform.parent = parent;
    }


    /// <summary>
    /// Урон наносимый оружием
    /// </summary>
    public float AttackDamage { get => attackDamage; }
    /// <summary>
    /// Максимальное расстояние при котором может происходить атака
    /// </summary>
    public float AttackDistance { get => attackDistance; }
    /// <summary>
    /// Время перезарядки оружия
    /// </summary>
    public float ReloadSpeed { get => reloadSpeed; }
}
