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
        executor.Animator?.Play(states.onStart.animationName);
        if (states.onStart.presset)
        {
            if (states.onStart.presset.Loop)
                executor.Source.loop = true;
            executor.Source.volume = states.onStart.presset.Volume;
            executor.Source.pitch = states.onStart.presset.Pitch;
            executor.Source.clip = states.onStart.presset.Clip;
            executor.Source?.Play();
        }
    }

    private void OnComplete(AI executor)
    {
        executor.Animator?.Play(states.onComplete.animationName);
        if (states.onComplete.presset)
        {
            if (states.onComplete.presset.Loop)
                executor.Source.loop = true;
            executor.Source.volume = states.onComplete.presset.Volume;
            executor.Source.pitch = states.onComplete.presset.Pitch;
            executor.Source.clip = states.onComplete.presset.Clip;
            executor.Source?.Play();
        }
    }

    protected override IEnumerator Action(AI executor, System.Action onComplete)
    {
        OnStart(executor);


        Vector3 endPosition = target.transform.position;
        if(!pursue)
        {
            executor.Agent.SetDestination(endPosition);
        }
        currentDistance = float.MaxValue;
        while (currentDistance > exitDistance)
        {
            if (pursue)
            {
                endPosition = target.transform.position;
                executor.Agent.SetDestination(endPosition);
            }

            //executor.Source.pitch = Random.Range(1f, 1.3f);

            currentDistance = Vector2.Distance(new Vector2(endPosition.x, endPosition.y), new Vector2(executor.Agent.transform.position.x, executor.Agent.transform.position.y));




            GameUtils.LookAt2D(executor.Presset.RotateParent, executor.transform.position + executor.Agent.velocity, executor.Presset.RotateOffset);


            yield return new WaitForFixedUpdate();
        }


        OnComplete(executor);
        onComplete();
        yield return null;
    }
}
