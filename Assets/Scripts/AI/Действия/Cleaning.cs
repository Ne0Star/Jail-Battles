using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaning : AIAction
{
    [SerializeField] private Entity executor;
    [SerializeField] private Trash trash;
    [SerializeField] private float cleaningTime;
    public Cleaning(Entity executor, Trash trash, float cleaningTime)
    {
        this.executor = executor;
        this.trash = trash;
        this.cleaningTime = cleaningTime;

    }

    public override void CustomUpdate()
    {
        if (!trash.IsFree)
        {
            onComplete?.Invoke(this);
        }
    }

    public override void Initial()
    {
        trash.StartCleaning(cleaningTime);
            GameUtils.LookAt2D(executor.Agent.transform, trash.transform.position, -90);
        executor.Animator.Play("cleaning");
    }
}
