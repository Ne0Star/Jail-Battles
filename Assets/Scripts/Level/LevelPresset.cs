using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Level Presset")]
public class LevelPresset : ScriptableObject
{
    [SerializeField] private int maxPursues = 10;
    [SerializeField] private  float pursueChance = 30f;

    /// <summary>
    /// ������������ ����������� ���������������
    /// </summary>
    public int MaxPursues { get => maxPursues; set => maxPursues = value; }
    /// <summary>
    /// ���� ��� ������ ������ ������������
    /// </summary>
    public float PursueChance { get => pursueChance; set => pursueChance = value; }
}
