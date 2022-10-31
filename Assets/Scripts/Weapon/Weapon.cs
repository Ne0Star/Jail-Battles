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
    [SerializeField] private bool free = false;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource source;
    [SerializeField] private Transform particles;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackDistance;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private WeaponType weaponType;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;
    }

    public WeaponType WeaponType { get => weaponType; }

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
    /// Максимальное расстояние при котором может происходить атака
    /// </summary>
    public float AttackDistance { get => attackDistance; }
    /// <summary>
    /// Время перезарядки оружия
    /// </summary>
    public float ReloadSpeed { get => reloadSpeed; }
    public bool Free { get => free; set => free = value; }
}
