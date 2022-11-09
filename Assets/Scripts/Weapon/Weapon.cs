using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum WeaponCategory
{
    /// <summary>
    /// Нету оружия
    /// </summary>
    None = 0,

    /// <summary>
    /// Пистолет
    /// </summary>
    Стрелковое_Легкое = 1,

    /// <summary>
    /// Автомат
    /// </summary>
    Стрелковое_Тяжелое = 2,

    /// <summary>
    /// Дубинка/короткая палка
    /// </summary>
    Ближний_Бой_Одноручное = 3,

    /// <summary>
    /// Палка, копьё...
    /// </summary>
    Ближний_Бой_Двуручное = 4,

    /// <summary>
    /// Граната, молотов
    /// </summary>
    Только_Метательное = 5
}

/// <summary>
/// Тип оружия
/// Категория = 14 
/// </summary>
[System.Serializable]
public enum WeaponType
{
    None,
    MP_443_Грач,
    АПС,
    Железная_Дилдинка,
    Макар,
    ПП_91_Кедр,
    Швабра,
    Граната,
    Кунай,
    Сюрикен,
    Палка,
    Сломанная_Палка,
    Бутылка,
    Вилка,
    Нож,
}

/// <summary>
/// Оружие
/// </summary>
public class Weapon : MonoBehaviour
{
    public WeaponType WeaponType { get => weaponType; }
    public WeaponCategory WeaponCategory { get => weaponCategory; }

    [SerializeField] protected bool free = false;
    [SerializeField] protected AudioClip clip;
    [SerializeField] protected AudioSource source;
    [SerializeField] protected float attackDamage;
    [SerializeField] private int attackCount;
    [SerializeField] protected float attackSpeed;
    [SerializeField] private float reloadSpeed;

    [SerializeField] private WeaponCategory weaponCategory;
    [SerializeField] private WeaponType weaponType;
    private void OnDrawGizmosSelected()
    {

    }

    private void OnValidate()
    {
        //weaponCategory = (WeaponCategory)weaponType.GetTypeCode();
    }



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
