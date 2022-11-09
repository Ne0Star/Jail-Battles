using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    [SerializeField] private bool init = false;
    [SerializeField] private Entity executor;
    [SerializeField] private float idleDuration;
    [SerializeField] private float currentTime;

    public IdleAction(Entity executor, float idleDuration)
    {
        this.executor = executor;
        this.idleDuration = idleDuration;
    }

    public IdleAction(Entity executor, float idleDuration, List<AIArea> aIAreas) : this(executor, idleDuration)
    {
        AIAreas = aIAreas;
    }

    public List<AIArea> AIAreas { get; }
    [SerializeField] private Vector3 randomPosition;
    public override void CustomUpdate()
    {
        if (randomPosition != Vector3.zero)
        {
            if(executor as Enemu)
            {
                Enemu e = (Enemu)executor;
                GameUtils.LookAt2DSmooth(e.RotateParent, randomPosition, e.RotateOffset, Time.unscaledDeltaTime * (e.Agent.speed * LevelManager.Instance.LevelData.RotateMultipler));
            }
        }
        if (currentTime >= idleDuration)
        {

            OnComplete?.Invoke(this);

            currentTime = 0f;
        }
        currentTime += Time.unscaledDeltaTime;
        if (init)
        {
            AIAreas[Random.Range(0, AIAreas.Count - 1)].GetVector(executor.Agent, (result) =>
           {
               randomPosition = result;
           });
            executor.Animator.Play("idle");
            init = false;
        }
    }

    [SerializeField] private int lastOrder;

    public override void Initial()
    {
        randomPosition = Vector3.zero;
        currentTime = 0;
        init = true;
        lastOrder = executor.Agent.avoidancePriority;
        executor.Agent.avoidancePriority = 1;
        executor.Agent.isStopped = true;
        onComplete?.AddListener(Complete);
    }

    private void Complete(AIAction action)
    {
        executor.Agent.avoidancePriority = lastOrder;
    }

}
