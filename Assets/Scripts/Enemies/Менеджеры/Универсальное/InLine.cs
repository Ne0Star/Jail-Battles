using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InlineType
{
    ВСтоловую,
    ВМедпункт,
    ВОбыскной
}

/// <summary>
/// Очередь AI
/// </summary>
public abstract class InLine : MonoBehaviour
{
    [SerializeField] protected InlineType type;
    [SerializeField] protected bool isEmpty = false;
    [SerializeField] private bool isCompleted = false;

    [SerializeField] private List<ExecutorAction> executors = new List<ExecutorAction>();

    [System.Serializable]
    private struct ExecutorAction
    {
        Entity executor;
        Action onComplete;

        public ExecutorAction(Entity executor, Action onComplete)
        {
            this.executor = executor;
            this.onComplete = onComplete;
        }

        public Action OnComplete { get => onComplete; }
        public Entity Executor { get => executor; }
    }

    public void AddExecutor(Entity executor, Action onComplete)
    {
        if (!executor || !executor.gameObject.activeSelf) return;
        executors.Add(new ExecutorAction(executor, onComplete));
        if (isEmpty)
        {
            StartCoroutine(Life());
            isEmpty = false;
        }
    }

    private void Awake()
    {
        isEmpty = true;
    }


    protected abstract void StartAction(Entity exucutor, System.Action<Entity> onComplete);

    private IEnumerator Life()
    {
        while (!isCompleted)
        {
            for (int i = 0; i < executors.Count; i++)
            {
                ExecutorAction e = executors[i];
                if (!e.Executor.gameObject.activeSelf)
                {
                    executors.Remove(e);
                    continue;
                }
                if (e.Executor as Enemu)
                {
                    Enemu enemu = (Enemu)e.Executor;
                    Transform target = i - 1 >= 0 ? executors[i - 1].Executor.Agent.transform : transform;

                    AIAction act = new MoveToTarget(enemu, target);
                    act.OnComplete.AddListener((a) =>
                    {

                    });
                    enemu.SetAction(act);
                }



                //e.Executor.Agent.isStopped = false;

                //Vector3 pos;



                //Vector3 resultPos = i - 1 >= 0 ?
                //    executors[i - 1].Executor.Agent.transform.position +
                //    GameUtils.ClampMagnitude(
                //    e.Executor.Agent.transform.position - executors[i - 1].Executor.Agent.transform.position,
                //    e.Executor.Agent.radius + executors[i - 1].Executor.Agent.radius,
                //    e.Executor.Agent.radius + executors[i - 1].Executor.Agent.radius
                //    )

                //    :

                //    transform.position;
                //e.Executor.Agent.SetDestination(resultPos);

                if (Vector2.Distance(e.Executor.Agent.transform.position, transform.position) <= e.Executor.Agent.radius * 2)
                {
                    StartAction(e.Executor, (Entity) =>
                    {
                        e.OnComplete?.Invoke();
                        executors.Remove(e);
                    });
                }
            }
            yield return new WaitForSeconds(1f);
        }

        isEmpty = true;
        yield return null;
    }
    public InlineType Type { get => type; }
}
