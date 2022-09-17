using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "AttackDealer")]
public class Attack : ScriptableObject
{
    [SerializeField] protected float damage;
    [SerializeField] protected string clipName;

    public string ClipName { get => clipName; set => clipName = value; }
}
