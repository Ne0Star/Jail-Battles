using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

/// <summary>
/// Двигаться по арене
/// </summary>

[System.Serializable]
public class ActionMoveByArea : AIAction
{

    [SerializeField] private AI executor;
    [SerializeField] private List<AIArea> areas;
    [SerializeField] private bool isMove;
    [SerializeField] private bool isIdle;
    public ActionMoveByArea(AI executor, List<AIArea> areas, AreaType type)
    {
        isMove = false;
        isIdle = false;
        this.executor = executor;
        List<AIArea> aIAreas = new List<AIArea>();
        foreach (AIArea area in areas)
        {
            if (area.AreaType == type)
            {
                aIAreas.Add(area);
            }
        }
        this.areas = aIAreas;
    }
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private float distance;

    public override void CustomUpdate()
    {
        GameUtils.LookAt2D(executor.RotateParent, executor.transform.position + executor.Agent.velocity, executor.RotateOffset);
        if (!isMove)
        {

            if (!isIdle)
            {
                executor.Stats.Idle.Play(executor.Animator, executor.Source, executor.Agent.speed);
                isIdle = true;
            }
            areas[Random.Range(0, areas.Count)].GetVector(executor.Agent, (result) =>
            {
                if (executor.Entity.gameObject.activeSelf)
                {
                    isMove = true;
                    targetPos = result;
                    executor.Stats.Walk.Play(executor.Animator, executor.Source, executor.Agent.speed);
                    executor.Agent.SetDestination(targetPos);
                    isIdle = false;
                }
            });
        }
        else
        {
            distance = Vector2.Distance(executor.transform.position, targetPos);
            if (distance <= executor.Agent.radius * 2)
            {
                onComplete?.Invoke(this);
                isMove = false;
                targetPos = Vector3.zero;
            }
        }
    }

    public override void Initial()
    {

    }

    public override void Break()
    {
        isMove = false;

    }








    //private AI executor;
    //private List<AIArea> areas;

    //private bool isMove = false;
    //[SerializeField] private Vector3 targetPos = Vector3.zero;
    //[SerializeField] private Vector3 executorPos = Vector3.zero;
    //public ActionMoveByArea(AI executor, List<AIArea> areas, AreaType type)
    //{
    //    this.executor = executor;
    //    List<AIArea> aIAreas = new List<AIArea>();
    //    foreach (AIArea area in areas)
    //    {
    //        if (area.AreaType == type)
    //        {
    //            aIAreas.Add(area);
    //        }
    //    }
    //    this.areas = aIAreas;
    //}
    //[SerializeField] private float distance;
    //public override void CustomUpdate()
    //{

    //    if (!isMove)
    //    {
    //        isMove = true;
    //        targetPos = areas[Random.Range(0, areas.Count)].GetVector();
    //        executor.Agent.SetDestination(targetPos);
    //    }
    //    else
    //    {
    //        Debug.Log("SYKA EBANAZ");
    //        GameUtils.LookAt2D(executor.Data.RotateParent, executor.transform.position + executor.Agent.velocity, executor.Data.RotateOffset);
    //        executorPos = executor.transform.position;
    //        distance = Vector2.Distance(executorPos, targetPos);
    //        if (distance <= executor.Agent.radius)
    //        {
    //            onComplete?.Invoke();
    //        }
    //    }

    //}

    //public override void Initial()
    //{
    //    Debug.Log("Init");
    //}
}
