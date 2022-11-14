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
public class WeaponStatInt
{
    [SerializeField] private bool allowUpdate = true;
    [SerializeField] private int value;
    [SerializeField] private int maxValue;

    [SerializeField] private int updateCount = 0;
    [SerializeField] private int maxUpdateCount = 10;

    [SerializeField] private float priceFactor = 0.01f;
    public int Value { get => GetValue(); }
    public int MaxValue { get => maxValue; }
    public int UpdateCount { get => updateCount; }
    public int MaxUpdateCount { get => maxUpdateCount; }
    public float PriceFactor { get => priceFactor; }
    public bool AllowUpdate { get => allowUpdate; }

    public void SetUpdate(int count)
    {
        if (!allowUpdate) return;
        updateCount = Mathf.Clamp(count, 1, maxUpdateCount);
        value = GetValue();
    }

    public void Update()
    {
        if (!allowUpdate) return;
        updateCount = Mathf.Clamp(updateCount + 1, 1, maxUpdateCount);
        value = GetValue();
    }


    private int GetValue()
    {
        float percent = updateCount * 100 / maxUpdateCount;
        float coof = maxValue / 100f;
        int totalValue = Mathf.RoundToInt(coof * percent);
        return totalValue;
    }

}

[System.Serializable]
public class WeaponStatFloat
{
    [SerializeField] private bool allowUpdate = true;

    [SerializeField] private float value;
    [SerializeField] private float maxValue;

    [SerializeField] private int updateCount = 0;
    [SerializeField] private int maxUpdateCount = 10;

    [SerializeField] private float priceFactor = 0.01f;

    public float Value { get => GetValue(); }
    public float MaxValue { get => maxValue; }

    public int UpdateCount { get => updateCount; }
    public int MaxUpdateCount { get => maxUpdateCount; }
    public bool AllowUpdate { get => allowUpdate; }
    public float PriceFactor { get => priceFactor; }

    public void SetUpdate(int count)
    {
        if (!allowUpdate) return;
        updateCount = Mathf.Clamp(count, 1, maxUpdateCount);
        value = GetValue();
    }

    public void Update()
    {
        if (!allowUpdate) return;
        updateCount = Mathf.Clamp(updateCount + 1, 1, maxUpdateCount);
        value = GetValue();
    }

    private float GetValue()
    {
        float percent = updateCount * 100 / maxUpdateCount;
        float coof = maxValue / 100f;
        float totalValue = coof * percent;
        return totalValue;
    }

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
    public bool Free { get => free; set => free = value; }
}
