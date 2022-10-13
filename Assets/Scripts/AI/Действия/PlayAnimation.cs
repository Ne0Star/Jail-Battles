using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DefaultAnimationsNames
{

}

public class PlayAnimation : AIAction
{
    [Header("ќжидать окончани€ анимации ? ")]
    [SerializeField] private bool waitEndAnimation;


    protected override IEnumerator Action(AI executor, Action onComplete)
    {
        string animationName = "";
        executor.Animator.Play(animationName);
        if (waitEndAnimation)
            yield return new WaitUntil(() => !executor.Animator.GetCurrentAnimatorStateInfo(0).IsName(animationName));

        onComplete();
    }
}
