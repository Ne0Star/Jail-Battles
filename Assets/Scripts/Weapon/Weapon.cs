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
public struct WeaponStat
{
    [Header("������������� ������ ��� ������� ���� ������")]
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
/// ������
/// </summary>
public class Weapon : MonoBehaviour
{

    public WeaponType WeaponType { get => weaponType; }
    public WeaponCategory WeaponCategory { get => weaponCategory; }

    [SerializeField] protected bool free = false;
    [SerializeField] protected AudioClip clip;
    [SerializeField] protected AudioSource source;

    [SerializeField] protected int currentUpdateNumber;


    [Header("������������ ���������� ��������� ��� ������")]
    [SerializeField] protected int maxUpdateCount = 10;
    [Header("������� ����� ���������")]
    [SerializeField] protected int currentUpdateCount = 0;
    [Header("������� ��������� ������")]
    [SerializeField] protected WeaponStat currentStat;
    [Header("������������")]
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

    /// <summary>
    /// ���� ��������� �������
    /// </summary>
    public float AttackDamage { get => currentStat.AttackDamage; }
    /// <summary>
    /// ����� ����������� ������
    /// </summary>
    public bool Free { get => free; set => free = value; }
    public float AttackSpeed { get => currentStat.AttackSpeed; }
    public int AttackCount { get => currentStat.AttackCount; }
    /// <summary>
    /// �������� �����������
    /// </summary>
    public float ReloadSpeed { get => currentStat.ReloadSpeed; }
    public WeaponStat FullStat { get => fullStat; }
    public int MaxUpdateCount { get => maxUpdateCount; }
    public int CurrentUpdateCount { get => currentUpdateCount; }
}
