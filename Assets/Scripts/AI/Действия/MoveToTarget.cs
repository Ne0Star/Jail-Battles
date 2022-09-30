using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToTarget : AIAction
{
    [SerializeField] private float currentDistance;
    [SerializeField] private float exitDistance;
    [SerializeField] private Transform target;
    protected override IEnumerator Action(AI executor, System.Action onComplete)
    {
        executor.Agent.SetDestination((Vector2)target.transform.position);

        currentDistance = float.MaxValue;
        while (currentDistance > exitDistance)
        {
            currentDistance = Vector2.Distance(new Vector2(target.transform.position.x, target.transform.position.y), new Vector2(executor.Agent.transform.position.x, executor.Agent.transform.position.y));
            GameUtils.LookAt2D(executor.Presset.RotateParent, target.transform.position + executor.Agent.velocity, executor.Presset.RotateOffset);
            yield return new WaitForFixedUpdate();
        }
        //Debug.Log("Complete");
        onComplete();
        yield return null;
    }
}
