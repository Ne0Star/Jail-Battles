using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// �������� ��� ai, ������� ���������� ai �� ��� ������ ��� �� �������� ���������� ��������
/// </summary>
/// 
[System.Serializable]
public abstract class AIAction : ScriptableObject
{
    [SerializeField] protected UnityEvent<AIAction> onComplete = new UnityEvent<AIAction>();
    [SerializeField] protected UnityEvent<AIAction> onBreak = new UnityEvent<AIAction>();
    public UnityEvent<AIAction> OnComplete { get => onComplete; }
    public UnityEvent<AIAction> OnBreak { get => onBreak; }
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
