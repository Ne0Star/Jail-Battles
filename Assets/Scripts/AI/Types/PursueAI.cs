using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Преследователь, или же просто зек
/// </summary>
public class PursueAI : AI
{
    [SerializeField] private Entity target = null;

    private void OnEnable()
    {
        free = true;
    }

    private void OnDisable()
    {
        free = false;
    }

    protected override void OnDamaged(Entity sources, float value)
    {

        if (Entity.gameObject.activeSelf)
        {
            if (consistency)
            {
                this.target = null;
                consistency.StopConsistency();
                OnCustomTriggerStay(sources);
            }
            else
            {
                if (free)
                {
                    this.target = null;
                    if(consistency)
                    consistency.StopConsistency();
                    OnCustomTriggerStay(sources);
                }
                else
                {
                    this.target = sources;
                }
            }
            //OnCustomTriggerStay(sources);
        }
    }

    protected override void OnCustomTriggerStay(Entity entity)
    {
        if (this.target != null || !Entity.gameObject.activeSelf || entity == Entity) return;


        bool pursue = Random.Range(0f, 100f) <= LevelManager.Instance.LevelPresset.PursueChance;
        if (!pursue) return;
        this.target = entity;
        free = false;
        if (consistency)
            consistency.StopConsistency();


        //if (target && target.gameObject.activeInHierarchy) return;
        //if(entity != null && entity.gameObject.activeInHierarchy)
        //this.target = entity;

        //free = false;
        //if (consistency)
        //    consistency.StopConsistency();
        consistency = null;

        StartCoroutine(Life());
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(agent.transform.position, (agent.radius + agent.radius) * 2);
        if (target)
        {
            Gizmos.color = Color.cyan;
            float r = (agent.radius + agent.radius) + 0.1f;
            Gizmos.DrawWireSphere(target.Agent.transform.position + GameUtils.ClampMagnitude((agent.transform.position - target.Agent.transform.position), r, r), 0.5f);
        }
    }
    [SerializeField] private float test;
    private IEnumerator Life()
    {
        AIStatPresset stat;
        while (target && target.gameObject.activeSelf && entity && entity.gameObject.activeSelf)
        {
            float r = (agent.radius + agent.radius);

            yield return new WaitForFixedUpdate();
            if (Vector2.Distance(target.Agent.transform.position, agent.transform.position) <= r + 0.1f)
            {
                GameUtils.LookAt2D(Presset.RotateParent, target.Agent.transform.position, Presset.RotateOffset);
                agent.isStopped = true;


                stat = presset.States.StandartStrikes[Random.Range(0, presset.States.StandartStrikes.Count)];
                Animator.Play(stat.Animation.animationName);
                Animator.speed = stat.Animation.speed;

                Source.loop = stat.Sound.Loop;
                Source.pitch = stat.Sound.Pitch;
                Source.volume = stat.Sound.Volume;
                Source.clip = stat.Sound.Clip;
                yield return new WaitForSeconds(stat.Animation.duration / 2);
                agent.obstacleAvoidanceType = UnityEngine.AI.ObstacleAvoidanceType.LowQualityObstacleAvoidance;
                Source.Play();
                yield return new WaitForSeconds(stat.Animation.duration / 2);
                stat = presset.States.Idle;
                Source.Stop();
                Animator.Play(stat.Animation.animationName);
                Animator.speed = stat.Animation.speed;
                if (target && entity)
                {
                    target.TakeDamage(entity, 10f);
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    break;
                }

            }
            else
            {
                agent.SetDestination(target.Agent.transform.position + GameUtils.ClampMagnitude((agent.transform.position - target.Agent.transform.position), r, r));
                GameUtils.LookAt2D(Presset.RotateParent, transform.position + agent.velocity, Presset.RotateOffset);
                agent.isStopped = false;
            }


        }

        //stat = presset.States.Idle;

        //Animator.Play(stat.Animation.animationName);
        //Animator.speed = stat.Animation.speed;


        //yield return new WaitForSeconds(1f);
        agent.isStopped = false;
        agent.obstacleAvoidanceType = UnityEngine.AI.ObstacleAvoidanceType.NoObstacleAvoidance;
        target = null;
        free = true;
    }

    protected override void UpdateAI()
    {

    }
}
