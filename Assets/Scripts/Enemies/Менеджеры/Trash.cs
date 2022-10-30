using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool cleaning = false;

    public event System.Action<Trash> onComplete;

    public void Cleaning()
    {
        if (cleaning) return;
        cleaning = true;
        animator.Play("Clean");
    }

    public void Complete()
    {
        onComplete(this);
    }

}
