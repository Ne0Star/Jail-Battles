using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private float notAttackDistance;



    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, attackDistance.Value);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, notAttackDistance);
    //}

    /// <summary>
    /// ���������� ��� ������� ����� �� ����� �����������
    /// </summary>
    public float NotAttackDistance { get => notAttackDistance; }

}
