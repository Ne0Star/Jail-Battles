using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��� ������
/// </summary>
public enum WeaponType
{
    ����������,
    ���������
}

/// <summary>
/// ������
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackDistance;
    [SerializeField] private float reloadSpeed;

    private Transform parent;

    [SerializeField] private WeaponType weaponType;

    public WeaponType WeaponType { get => weaponType; }

    private void Start()
    {
        parent = transform.parent;
    }

    private void OnDisable()
    {
        transform.parent = parent;
    }


    /// <summary>
    /// ���� ��������� �������
    /// </summary>
    public float AttackDamage { get => attackDamage; }
    /// <summary>
    /// ������������ ���������� ��� ������� ����� ����������� �����
    /// </summary>
    public float AttackDistance { get => attackDistance; }
    /// <summary>
    /// ����� ����������� ������
    /// </summary>
    public float ReloadSpeed { get => reloadSpeed; }
}
