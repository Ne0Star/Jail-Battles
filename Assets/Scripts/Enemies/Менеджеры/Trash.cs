using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] public bool block = false;
    [SerializeField] private Animator animator;
    [SerializeField] private bool cleaning = false;

    public event System.Action<Trash> onComplete;

    public void Cleaning()
    {
        if (cleaning) return;
        block = true;
        cleaning = true;
        animator.Play("Clean");
    }

    public void Complete()
    {
        onComplete(this);
        gameObject.SetActive(false);
        block = false;
        cleaning = false;
    }

}
