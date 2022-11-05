using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIActionItem : MonoBehaviour
{
    [SerializeField] protected UnityEvent<AIActionItem> onComplete;
    [SerializeField] protected bool isFree = false;

    public bool IsFree { get => isFree; }
    public UnityEvent<AIActionItem> OnComplete { get => onComplete; }
}
