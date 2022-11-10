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

/// <summary>
/// ��� ������
/// ��������� = 14 
/// </summary>
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

    public float AttackDamage { get => attackDamage; }
    public int AttackCount { get => attackCount; }
    public float AttackSpeed { get => attackSpeed; }
    public float ReloadSpeed { get => reloadSpeed; }




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
    [SerializeField] protected WeaponStat[] updateStats = new WeaponStat[1];

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
    public float AttackDamage { get => updateStats[currentUpdateNumber].AttackDamage; }
    /// <summary>
    /// ����� ����������� ������
    /// </summary>
    public bool Free { get => free; set => free = value; }
    public float AttackSpeed { get => updateStats[currentUpdateNumber].AttackSpeed; }
    public int AttackCount { get => updateStats[currentUpdateNumber].AttackCount; }
    /// <summary>
    /// �������� �����������
    /// </summary>
    public float ReloadSpeed { get => updateStats[currentUpdateNumber].ReloadSpeed; }
    public WeaponStat[] UpdateStats { get => updateStats; }
}
