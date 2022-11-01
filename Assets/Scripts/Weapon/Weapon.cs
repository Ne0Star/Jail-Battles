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
    public WeaponType WeaponType { get => weaponType; }

    [SerializeField] protected bool free = false;
    [SerializeField] protected AudioClip clip;
    [SerializeField] protected AudioSource source;
    [SerializeField] protected float attackDamage;
    [SerializeField] private int attackCount;
    [SerializeField] protected float attackSpeed;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private GameObject left, top;

    /// <summary>
    /// Переключает отображение оружия на вид сбоку
    /// </summary>
    public void Left()
    {
        top.gameObject.SetActive(false);
        left.gameObject.SetActive(true);
    }
    /// <summary>
    /// Переключает отображение оружия на вид сверху
    /// </summary>
    public void Top()
    {
        top.gameObject.SetActive(true);
        left.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;
    }

    public void AnimateAttack()
    {
        source.clip = clip;
        source.Play();

    }

    /// <summary>
    /// Урон наносимый оружием
    /// </summary>
    public float AttackDamage { get => attackDamage; }
    /// <summary>
    /// Время перезарядки оружия
    /// </summary>
    public bool Free { get => free; set => free = value; }
    public float AttackSpeed { get => attackSpeed; }
    public int AttackCount { get => attackCount; }
    /// <summary>
    /// Скорость перезарядки
    /// </summary>
    public float ReloadSpeed { get => reloadSpeed; }
}
