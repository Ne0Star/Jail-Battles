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
    public bool hiden = false;

    [LabelOverride("Улучшаемый ? ")]
    [SerializeField] private bool allowUpdate = true;
    private int value;
    [LabelOverride("Максимум ")]
    [SerializeField] private int maxValue;
    [LabelOverride("Минимум ")]
    [SerializeField] private int minValue;
    [LabelOverride("Текущее улучшение")]
    [SerializeField] private int updateCount = 0;
    [LabelOverride("Максимальное улучшение")]
    [SerializeField] private int maxUpdateCount = 10;

    public int Value { get => value; }
    public int MaxValue { get => maxValue; set => this.maxValue = value; }
    public int UpdateCount { get => updateCount; set => this.updateCount = value; }
    public int MaxUpdateCount { get => maxUpdateCount; set => this.maxUpdateCount = value; }
    public bool AllowUpdate { get => allowUpdate; set => this.allowUpdate = value; }
    public int MinValue { get => minValue; set => this.minValue = value; }

    public void SetUpdate(int count)
    {
        if (!allowUpdate) return;
        updateCount = Mathf.Clamp(count, 1, maxUpdateCount);
        value = GetValue();
    }

    public void UpdateValue()
    {
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
        float coof = Mathf.InverseLerp(0f, maxUpdateCount, updateCount);
        float total = Mathf.Lerp(minValue, maxValue, coof);
        return Mathf.FloorToInt(total);
    }

}

[System.Serializable]
public class WeaponStatFloat
{
    public bool hiden = false;

    [LabelOverride("Улучшаемый ? ")]
    [SerializeField] private bool allowUpdate = true;
    private float value;
    [LabelOverride("Максимум ")]
    [SerializeField] private float maxValue;
    [LabelOverride("Минимум ")]
    [SerializeField] private float minValue;

    [LabelOverride("Текущее улучшение")]
    [SerializeField] private int updateCount = 0;
    [LabelOverride("Максимальное улучшение")]
    [SerializeField] private int maxUpdateCount = 10;

    public float Value { get => value; }
    public float MaxValue { get => maxValue; set => this.maxValue = value; }
    public int UpdateCount { get => updateCount; set => this.updateCount = value; }
    public int MaxUpdateCount { get => maxUpdateCount; set => this.maxUpdateCount = value; }
    public bool AllowUpdate { get => allowUpdate; set => this.allowUpdate = value; }
    public float MinValue { get => minValue; set => this.minValue = value; }

    public void SetUpdate(int count)
    {
        if (!allowUpdate) return;
        updateCount = Mathf.Clamp(count, 1, maxUpdateCount);
        value = GetValue();
    }
    public void UpdateValue()
    {
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
        float coof = Mathf.InverseLerp(0f, maxUpdateCount, updateCount);
        float total = Mathf.Lerp(minValue, maxValue, coof);
        return total;
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
