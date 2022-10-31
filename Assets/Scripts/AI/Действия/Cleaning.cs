using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaning : AIAction
{
    [SerializeField] private Entity executor;
    [SerializeField] private Trash trash;
    public Cleaning(Entity executor, Trash trash)
    {
        this.executor = executor;
        this.trash = trash;
    }

    public override void Break()
    {

    }

    public override void CustomUpdate()
    {
        if(!trash.block)
        {
            onComplete?.Invoke(this);
        }
    }

    public override void Initial()
    {
        trash.Cleaning();
        executor.Animator.Play("cleaning");
    }
}
