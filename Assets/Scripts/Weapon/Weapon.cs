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
[System.Serializable]
public struct WeaponStat
{
    [Header("Универсальные данные для каждого типа оружия")]
    [SerializeField] private float attackDamage;
    [SerializeField] private int attackCount;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float reloadSpeed;

    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public int AttackCount { get => attackCount; set => attackCount = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float ReloadSpeed { get => reloadSpeed; set => reloadSpeed = value; }
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

    [SerializeField] protected int currentUpdateNumber;


    [Header("Максимальное количество улучшений для оружия")]
    [SerializeField] protected int maxUpdateCount = 10;
    [Header("Текущий номер улучшения")]
    [SerializeField] protected int currentUpdateCount = 0;
    [Header("Текущее улучшение оружия")]
    [SerializeField] protected WeaponStat currentStat;
    [Header("Максимальное")]
    [SerializeField] protected WeaponStat fullStat;

    public void SetUpdate(int count)
    {
        currentUpdateCount = count;
        float percent = count * 100 / maxUpdateCount;

        currentStat.AttackCount = Mathf.RoundToInt(GetUpdate(percent, fullStat.AttackCount));
        currentStat.AttackSpeed = GetUpdate(percent, fullStat.AttackSpeed);
        currentStat.ReloadSpeed = GetUpdate(percent, fullStat.ReloadSpeed);
        currentStat.AttackDamage = GetUpdate(percent, fullStat.AttackDamage);
    }

    private float GetUpdate(float percent, float maxValue)
    {
        float coof = maxValue / 100f;
        float totalValue = coof * percent;
        return totalValue;
    }


    [SerializeField] private WeaponCategory weaponCategory;
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
    public float AttackDamage { get => currentStat.AttackDamage; }
    /// <summary>
    /// Время перезарядки оружия
    /// </summary>
    public bool Free { get => free; set => free = value; }
    public float AttackSpeed { get => currentStat.AttackSpeed; }
    public int AttackCount { get => currentStat.AttackCount; }
    /// <summary>
    /// Скорость перезарядки
    /// </summary>
    public float ReloadSpeed { get => currentStat.ReloadSpeed; }
    public WeaponStat FullStat { get => fullStat; }
    public int MaxUpdateCount { get => maxUpdateCount; }
    public int CurrentUpdateCount { get => currentUpdateCount; }
}
