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
    /// –ассто€ние при котором атака не может происходить
    /// </summary>
    public float NotAttackDistance { get => notAttackDistance; }

}
