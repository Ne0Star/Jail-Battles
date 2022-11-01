using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float notAttackDistance;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, notAttackDistance);
    }

    /// <summary>
    /// ������������ ���������� ��� ������� ����� ����������� �����
    /// </summary>
    public float AttackDistance { get => attackDistance; }
    /// <summary>
    /// ���������� ��� ������� ����� �� ����� �����������
    /// </summary>
    public float NotAttackDistance { get => notAttackDistance; }
}
