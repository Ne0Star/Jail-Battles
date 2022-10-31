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
    [SerializeField] private bool free = false;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource source;
    [SerializeField] private Transform particles;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackDistance;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private WeaponType weaponType;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;
    }

    public WeaponType WeaponType { get => weaponType; }

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
    /// ������������ ���������� ��� ������� ����� ����������� �����
    /// </summary>
    public float AttackDistance { get => attackDistance; }
    /// <summary>
    /// ����� ����������� ������
    /// </summary>
    public float ReloadSpeed { get => reloadSpeed; }
    public bool Free { get => free; set => free = value; }
}
