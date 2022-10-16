using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToTarget : AIAction
{
    [SerializeField] private bool pursue;
    [SerializeField] private float currentDistance;
    [SerializeField] private float exitDistance;
    [SerializeField] private Transform target;
    private void OnStart(AI executor)
    {
        if (!states) return;
        if (states.AStart)
        {
            executor.Animator.Play(states.AStart.animationName);
            executor.Animator.speed = states.AStart.speed;
        }
        if (states.SStart)
        {
            if (states.SStart)
                executor.Source.loop = true;

            executor.Source.volume = states.SStart.Volume;
            executor.Source.pitch = states.SStart.Pitch;
            executor.Source.clip = states.SStart.Clip;
            executor.Source?.Play();
        }
    }
    private void OnComplete(AI executor)
    {
        if (!states) return;
        if (states.AComplete)
        {
            executor.Animator.Play(states.AComplete.animationName);
            executor.Animator.speed = states.AComplete.speed;
        }
        if (states.SComplete)
        {
            if (states.SComplete.Loop)
                executor.Source.loop = true;
            executor.Source.volume = states.SComplete.Volume;
            executor.Source.pitch = states.SComplete.Pitch;
            executor.Source.clip = states.SComplete.Clip;
            executor.Source?.Play();
        }
    }
    protected override IEnumerator Action(AI executor, System.Action onComplete)
    {


        Vector3 endPosition = target.transform.position;
        if (!pursue)
        {
            executor.Agent.SetDestination(endPosition);
        }
        executor.Agent.obstacleAvoidanceType = UnityEngine.AI.ObstacleAvoidanceType.NoObstacleAvoidance;
        AIStatPresset stat = executor.Presset.States.Walk;
        executor.Source.clip = stat.Sound.Clip;
        executor.Source.loop = stat.Sound.Loop;
        executor.Source.volume = stat.Sound.Volume;
        executor.Source.pitch = stat.Sound.Pitch;
        executor.Source.Play();
        executor.Animator.Play(stat.Animation.animationName);
        executor.Animator.speed = stat.Animation.speed;
        currentDistance = float.MaxValue;



        while (currentDistance > exitDistance)
        {
            if (executor == null || !executor.gameObject || !executor.gameObject.activeSelf)
                yield return null;
            if (pursue)
            {
                endPosition = target.transform.position;
                executor.Agent.SetDestination(endPosition);
            }
            currentDistance = Vector2.Distance(new Vector2(endPosition.x, endPosition.y), new Vector2(executor.Agent.transform.position.x, executor.Agent.transform.position.y));
            try
            {
            GameUtils.LookAt2D(executor.Presset.RotateParent, executor.transform.position + executor.Agent.velocity, executor.Presset.RotateOffset);
            } catch
            {
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
        Debug.Log("PRISLA PIZDA");
        executor.Source.Stop();
        OnStart(executor);
        yield return new WaitForSeconds(duration);
        OnComplete(executor);
        onComplete();
        executor.Agent.obstacleAvoidanceType = UnityEngine.AI.ObstacleAvoidanceType.MedQualityObstacleAvoidance;
        yield return null;
    }

}
