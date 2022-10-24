using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// �������� ��� ai, ������� ���������� ai �� ��� ������ ��� �� �������� ���������� ��������
/// </summary>
/// 
[System.Serializable]
public abstract class AIAction //: ScriptableObject
{
    [SerializeField] protected UnityEvent onComplete = new UnityEvent();
    [SerializeField] protected UnityEvent onBreak = new UnityEvent();
    public UnityEvent OnComplete { get => onComplete; }
    public UnityEvent OnBreak { get => onBreak; }
    /// <summary>
    /// ���������� ��� ���������� �������� ������ ���� AI
    /// </summary>
    public abstract void Initial();
    /// <summary>
    /// ���������� ����� �������� �����������
    /// </summary>
    public abstract void Break();
    /// <summary>
    /// ���������� ������ ����� ���������� ��� ��������
    /// </summary>
    public abstract void CustomUpdate();
}
