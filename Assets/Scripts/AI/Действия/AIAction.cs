using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Действие для ai, дергает переданный ai за его рычаги что бы добиться выполнения действия
/// </summary>
/// 
[System.Serializable]
public abstract class AIAction// : ScriptableObject
{
    [SerializeField] protected UnityEvent<AIAction> onComplete = new UnityEvent<AIAction>();
    [SerializeField] protected UnityEvent<AIAction> onBreak = new UnityEvent<AIAction>();
    public UnityEvent<AIAction> OnComplete { get => onComplete; }
    public UnityEvent<AIAction> OnBreak { get => onBreak; }
    /// <summary>
    /// Вызывается при назначении действия какому либо AI
    /// </summary>
    public abstract void Initial();
    /// <summary>
    /// Вызывается когда действие прерывается
    /// </summary>
    public abstract void Break();
    /// <summary>
    /// Вызывается каждое время обновления для действия
    /// </summary>
    public abstract void CustomUpdate();
}
