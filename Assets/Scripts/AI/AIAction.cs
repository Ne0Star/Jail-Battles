using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Действие для ai, дергает переданный ai за его рычаги что бы добиться выполнения действия
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
    /// Вызывается при назначении действия какому либо AI
    /// </summary>
    public abstract void Initial();
    /// <summary>
    /// Вызывается каждое время обновления для действия
    /// </summary>
    public abstract void CustomUpdate();
}
