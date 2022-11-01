using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��� ������
/// </summary>
public enum WeaponType
{
    /// <summary>
    /// ���� ������
    /// </summary>
    None,
    /// <summary>
    /// ��������
    /// </summary>
    Gun,
    /// <summary>
    /// �������
    /// </summary>
    Machine,
    /// <summary>
    /// �������/�������� �����
    /// </summary>
    Mele,
}

/// <summary>
/// ������
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
    public float AttackDamage { get => attackDamage; }
    /// <summary>
    /// ����� ����������� ������
    /// </summary>
    public bool Free { get => free; set => free = value; }
    public float AttackSpeed { get => attackSpeed; }
    public int AttackCount { get => attackCount; }
    /// <summary>
    /// �������� �����������
    /// </summary>
    public float ReloadSpeed { get => reloadSpeed; }
}
