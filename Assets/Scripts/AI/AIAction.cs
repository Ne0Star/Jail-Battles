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
    public int id;
    [SerializeField] private bool blockStack = false;
    [SerializeField] protected UnityEvent<AIAction> onComplete = new UnityEvent<AIAction>();
    public UnityEvent<AIAction> OnComplete { get => onComplete; }
    public bool BlockStack { get => blockStack; set => blockStack = value; }

    /// <summary>
    /// ���������� ��� ���������� �������� ������ ���� AI
    /// </summary>
    public abstract void Initial();
    /// <summary>
    /// ���������� ������ ����� ���������� ��� ��������
    /// </summary>
    public abstract void CustomUpdate();
}
