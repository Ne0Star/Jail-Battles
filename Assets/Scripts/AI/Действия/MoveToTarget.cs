using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToTarget : AIAction
{
    [SerializeField] private float currentDistance;
    [SerializeField] private float exitDistance;
    [SerializeField] private Transform target;



    private void OnStart(AI executor)
    {
        executor.Animator?.Play(states.onStart.animationName);
        if (states.onStart.sound)
        {
            if (states.repearStartSound)
                executor.Source.loop = true;
            executor.Source.volume = states.startSoundValue;
            executor.Source.pitch = states.startSoundPitch;

            executor.Source.clip = states.onStart.sound;
            executor.Source?.Play();
        }
    }

    private void OnComplete(AI executor)
    {
        executor.Animator?.Play(states.onComplete.animationName);
        if (states.onComplete.sound)
        {
            executor.Source?.PlayOneShot(states.onComplete.sound);
        }
    }

    protected override IEnumerator Action(AI executor, System.Action onComplete)
    {
        OnStart(executor);


        
        currentDistance = float.MaxValue;
        while (currentDistance > exitDistance)
        {
executor.Agent.SetDestination((Vector2)target.transform.position);
            executor.Source.pitch = Random.Range(1f, 1.3f);

            currentDistance = Vector2.Distance(new Vector2(target.transform.position.x, target.transform.position.y), new Vector2(executor.Agent.transform.position.x, executor.Agent.transform.position.y));




            GameUtils.LookAt2D(executor.Presset.RotateParent, executor.transform.position + executor.Agent.velocity, executor.Presset.RotateOffset);


            yield return new WaitForFixedUpdate();
        }


        OnComplete(executor);
        onComplete();
        yield return null;
    }
}
