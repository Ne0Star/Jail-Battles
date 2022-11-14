using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum WeaponCategory
{
    /// <summary>
    /// ���� ������
    /// </summary>
    None = 0,

    /// <summary>
    /// ��������
    /// </summary>
    ����������_������ = 1,

    /// <summary>
    /// �������
    /// </summary>
    ����������_������� = 2,

    /// <summary>
    /// �������/�������� �����
    /// </summary>
    �������_���_���������� = 3,

    /// <summary>
    /// �����, �����...
    /// </summary>
    �������_���_��������� = 4,

    /// <summary>
    /// �������, �������
    /// </summary>
    ������_����������� = 5
}

[System.Serializable]
public enum WeaponType
{
    None,
    MP_443_����,
    ���,
    ��������_��������,
    �����,
    ��_91_����,
    ������,
    �������,
    �����,
    �������,
    �����,
    ���������_�����,
    �������,
    �����,
    ���,
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
/// ������
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
    /// ����������� ����������� ������ �� ��� �����
    /// </summary>
    public void Left()
    {
        top.gameObject.SetActive(false);
        left.gameObject.SetActive(true);
    }
    /// <summary>
    /// ����������� ����������� ������ �� ��� ������
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
