using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VisibleEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onVisible, onInvisible;

    private void Start()
    {
        onInvisible?.Invoke();
    }
    private void OnBecameInvisible()
    {
        onInvisible?.Invoke();
    }
    private void OnBecameVisible()
    {
        onVisible?.Invoke();
    }
}
