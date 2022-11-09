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
        if (!init) return;
        if (trash)
        {
            if (!startMove)
            {
                executor.Agent.SetDestination(trash.worldPos);
                executor.Animator.Play("walk");


                startMove = true;
            }
            if (!trash.gameObject.activeSelf)
            {
                onComplete?.Invoke(this);
                return;
            }
            if (Vector2.Distance(executor.Agent.transform.position, trash.worldPos) <= executor.Agent.radius + 0.1f)
            {
                if (!startCleaning)
                {
                    executor.Animator.Play("cleaning");
                    trash.StartCleaning(cleaningTime);
                    startCleaning = true;
                }
            }
            if (executor as Enemu)
            {
                Enemu executor = (Enemu)this.executor;
                if (!startCleaning)
                {
                    GameUtils.LookAt2DSmooth(executor.RotateParent, executor.Agent.transform.position + executor.Agent.velocity, executor.RotateOffset, Time.unscaledDeltaTime * (executor.Agent.speed * LevelManager.Instance.LevelData.RotateMultipler));
                }
                else
                {
                    GameUtils.LookAt2DSmooth(executor.RotateParent, trash.worldPos, executor.RotateOffset, Time.unscaledDeltaTime * (executor.Agent.speed * LevelManager.Instance.LevelData.RotateMultipler));
                }
            }
        }
        else
        {
            onComplete?.Invoke(this);
        }
    }
    private bool startCleaning = false;
    private bool startMove = false;
    private bool init = false;
    public override void Initial()
    {
        init = true;
    }
}
