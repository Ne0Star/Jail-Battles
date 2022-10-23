using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Преследователь, или же просто зек
/// Бродит по тюрьме, имеет шанс напасть на другого зека или на игрока, время от времени отходит в туалет
/// </summary>
public class PursueAI : AI
{
    [SerializeField] private bool pursue = false;


    [SerializeField] private int index;
    [SerializeField] private List<AIAction> lifeActions = new List<AIAction>();
    [SerializeField] private List<AIAction> stackActions = new List<AIAction>();
    [SerializeField] private AIAction currentAction;
    [SerializeField] private AIAction lastAction = null;

    /// <summary>
    /// Текущее действие перенести в стек и начать выполнение нового действия
    /// Когда новое действиие закончиться вернуться к последнему действию из стека
    /// </summary>
    /// <param name="entity"></param>
    protected override void OnCustomTriggerStay(Entity entity)
    {
        if (pursue || entity == this.entity || currentAction == null) return;
        pursue = true;

        lifeActions[index].Break();
        stackActions.Add(lifeActions[index]);

        currentAction = new ActionMoveToTarget(this, entity, 5f);
        currentAction.OnComplete.AddListener(() =>
        {
            currentAction = stackActions[stackActions.Count - 1];
            stackActions.RemoveAt(stackActions.Count - 1);
            pursue = false;
        });

        //int i = index > lifeActions.Count ? 0 : index;

        //if (!stackActions.Contains(lifeActions[i]))
        //    stackActions.Add(lifeActions[i]);


        //currentAction = new ActionMoveToTarget(this, entity, 5f);
        //currentAction.OnComplete.AddListener(() =>
        //{
        //    currentAction = stackActions[stackActions.Count - 1];
        //    pursue = false;
        //});
    }

    protected override void Create()
    {
        lifeActions.Add(new ActionMoveByArea(
             this,
             LevelManager.Instance.AiManager.Areas,
             AreaType.Столовая
             ));
    }
    protected override void Enable()
    {

    }
    protected override void Disable()
    {

    }

    protected override void UpdateAI()
    {

        //if (stackActions.Count > 0 && currentAction != null)
        //{
        //    Debug.Log("Есть незаконченные действия, но текущее дейсвие ещё не выполнено");
        //}
        //else if (stackActions.Count > 0 && currentAction == null)
        //{
        //    Debug.Log("Есть незаконченные действия, и текущее действие не выполняется !!");
        //}


        if (currentAction != null)
        {
            currentAction.CustomUpdate();
        }
        else
        {
            index = index + 1 > lifeActions.Count - 1 ? 0 : index + 1;
            currentAction = lifeActions[index];
        }
        if (currentAction != lastAction && currentAction != null)
        {
            currentAction.Initial();

            currentAction.OnComplete?.AddListener(() =>
            {
                currentAction = null;
            });

            lastAction = currentAction;
        }

    }


    //[SerializeField] private Entity target = null;
    //[SerializeField] protected AIStatPresset currentStat;


    //private bool CircleCollision(Vector3 pos1, Vector3 pos2, float rad1, float rad2)
    //{
    //    float a = pos1.x - pos2.x;
    //    float b = pos1.y - pos2.y;
    //    float c = (float)Mathf.Sqrt((float)(Mathf.Pow(a, 2) + Mathf.Pow(b, 2)));
    //    if ((rad1 + rad2 + 0.05f) >= c)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}


    //[SerializeField] private bool moveToTarget = false, attackTarget = false;


    //protected override void OnCustomTriggerStay(Entity entity)
    //{
    //    if (moveToTarget || entity == Entity) return;
    //    this.target = entity;
    //    if (consistency)
    //    {
    //        consistency.StopConsistency();
    //    }
    //    free = false;
    //    moveToTarget = true;
    //}

    //protected override void OnDamaged(Entity sources, float value)
    //{

    //}
    //private void SetStat(AIStatPresset stat)
    //{
    //    if (currentStat == null) return;
    //    if (currentStat.Sound)
    //    {
    //        Source.clip = currentStat.Sound.Clip;
    //        Source.volume = currentStat.Sound.Volume;
    //        Source.pitch = currentStat.Sound.Pitch;
    //        Source.loop = currentStat.Sound.Loop;
    //    }
    //    if (currentStat.Animation)
    //    {
    //        Animator.speed = currentStat.Animation.speed;
    //        Animator.Play(currentStat.Animation.animationName);
    //    }
    //}
    //protected override void UpdateAI()
    //{
    //    if (!target || !target.gameObject.activeSelf) moveToTarget = false;
    //    if (attackTarget)
    //    {
    //        SetStat(presset.States.FigtStance);


    //        return;
    //    }
    //    if (moveToTarget)
    //    {
    //        SetStat(presset.States.Walk);
    //        float totalRadius = agent.radius + target.Agent.radius;
    //        Vector3 targetPos = target.Agent.transform.position + GameUtils.ClampMagnitude(agent.transform.position - target.Agent.nextPosition, totalRadius, float.MaxValue);
    //        agent.SetDestination(targetPos);
    //        if (Vector2.Distance(agent.transform.position, targetPos) <= totalRadius)
    //        {
    //            attackTarget = true;
    //        }
    //    }
    //}


    //private void OnEnable()
    //{
    //    free = true;
    //}

    //private void OnDisable()
    //{
    //    free = false;
    //}

    //protected override void OnDamaged(Entity sources, float value)
    //{

    //    if (Entity.gameObject.activeSelf)
    //    {
    //        if (consistency)
    //        {
    //            this.target = null;
    //            consistency.StopConsistency();
    //            OnCustomTriggerStay(sources);
    //        }
    //        else
    //        {
    //            if (free)
    //            {
    //                this.target = null;
    //                if(consistency)
    //                consistency.StopConsistency();
    //                OnCustomTriggerStay(sources);
    //            }
    //            else
    //            {
    //                this.target = sources;
    //            }
    //        }
    //        //OnCustomTriggerStay(sources);
    //    }
    //}

    //protected override void OnCustomTriggerStay(Entity entity)
    //{
    //    if (this.target != null || !Entity.gameObject.activeSelf || entity == Entity) return;


    //    bool pursue = Random.Range(0f, 100f) <= LevelManager.Instance.LevelPresset.PursueChance;
    //    if (!pursue) return;
    //    this.target = entity;
    //    free = false;
    //    if (consistency)
    //        consistency.StopConsistency();

    //    consistency = null;

    //    StartCoroutine(Life());
    //}
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(agent.transform.position, (agent.radius + agent.radius) * 2);
    //    if (target)
    //    {
    //        Gizmos.color = Color.cyan;
    //        float r = (agent.radius + agent.radius) + 0.1f;
    //        Gizmos.DrawWireSphere(target.Agent.transform.position + GameUtils.ClampMagnitude((agent.transform.position - target.Agent.transform.position), r, r), 0.5f);
    //    }
    //}
    //[SerializeField] private float test;
    //private IEnumerator Life()
    //{
    //    AIStatPresset stat;
    //    while (target && target.gameObject.activeSelf && entity && entity.gameObject.activeSelf)
    //    {
    //        float r = (agent.radius + agent.radius);

    //        yield return new WaitForFixedUpdate();
    //        if (Vector2.Distance(target.Agent.transform.position, agent.transform.position) <= r + 0.1f)
    //        {
    //            GameUtils.LookAt2D(Presset.RotateParent, target.Agent.transform.position, Presset.RotateOffset);
    //            agent.isStopped = true;


    //            stat = presset.States.StandartStrikes[Random.Range(0, presset.States.StandartStrikes.Count)];
    //            Animator.Play(stat.Animation.animationName);
    //            Animator.speed = stat.Animation.speed;

    //            Source.loop = stat.Sound.Loop;
    //            Source.pitch = stat.Sound.Pitch;
    //            Source.volume = stat.Sound.Volume;
    //            Source.clip = stat.Sound.Clip;
    //            yield return new WaitForSeconds(stat.Animation.duration / 2);
    //            agent.obstacleAvoidanceType = UnityEngine.AI.ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    //            Source.Play();
    //            yield return new WaitForSeconds(stat.Animation.duration / 2);
    //            stat = presset.States.Idle;
    //            Source.Stop();
    //            Animator.Play(stat.Animation.animationName);
    //            Animator.speed = stat.Animation.speed;
    //            if (target && entity)
    //            {
    //                target.TakeDamage(entity, 10f);
    //                yield return new WaitForSeconds(1);
    //            }
    //            else
    //            {
    //                break;
    //            }

    //        }
    //        else
    //        {
    //            agent.SetDestination(target.Agent.transform.position + GameUtils.ClampMagnitude((agent.transform.position - target.Agent.transform.position), r, r));
    //            GameUtils.LookAt2D(Presset.RotateParent, transform.position + agent.velocity, Presset.RotateOffset);
    //            agent.isStopped = false;
    //        }


    //    }

    //    //stat = presset.States.Idle;

    //    //Animator.Play(stat.Animation.animationName);
    //    //Animator.speed = stat.Animation.speed;


    //    //yield return new WaitForSeconds(1f);
    //    agent.isStopped = false;
    //    agent.obstacleAvoidanceType = UnityEngine.AI.ObstacleAvoidanceType.NoObstacleAvoidance;
    //    target = null;
    //    free = true;
    //}
}
