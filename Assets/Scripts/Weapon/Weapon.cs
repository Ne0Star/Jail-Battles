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
    public bool hiden;
    public float updatePriceMultipler;
    [LabelOverride("���������� ? ")]
    [SerializeField] private bool allowUpdate;
    private int value;
    [LabelOverride("�������� ")]
    [SerializeField] private int maxValue;
    [LabelOverride("������� ")]
    [SerializeField] private int minValue;
    [LabelOverride("������� ���������")]
    [SerializeField] private int updateCount;
    [LabelOverride("������������ ���������")]
    [SerializeField] private int maxUpdateCount;

    //public WeaponStatInt(int updateCount, int maxUpdateCount) : this()
    //{
    //    hiden = false;
    //    updateCount = 0;
    //    allowUpdate = true;
    //    maxUpdateCount = 10;
    //}


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
    public bool hiden;
    public float updatePriceMultipler;

    [LabelOverride("���������� ? ")]
    [SerializeField] private bool allowUpdate;
    private float value;
    [LabelOverride("�������� ")]
    [SerializeField] private float maxValue;
    [LabelOverride("������� ")]
    [SerializeField] private float minValue;

    [LabelOverride("������� ���������")]
    [SerializeField] private int updateCount;
    [LabelOverride("������������ ���������")]
    [SerializeField] private int maxUpdateCount;

    //public WeaponStatFloat(float updateCount, float maxUpdateCount) : this()
    //{
    //    hiden = false;
    //    updateCount = 0;
    //    allowUpdate = true;
    //    maxUpdateCount = 10;
    //}
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

        Debug.Log("Mmmm" + value);
    }

    private float GetValue()
    {
        float coof = Mathf.InverseLerp(0f, maxUpdateCount, updateCount);
        float total = Mathf.Lerp(minValue, maxValue, coof);
        return total;
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
