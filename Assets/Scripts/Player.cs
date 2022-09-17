using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Entity
{

    //[SerializeField] private NavMeshAgent agent;

    //[SerializeField] private float physicalPower;
    //[SerializeField] private float forwardSpeed, backwardSpeed;
    //[SerializeField] private float rotateOffset;
    //[SerializeField] private bool isMove = false, isIdle = true, isFightingStance = false;
    //[SerializeField] private AttackController isAttack;
    //[SerializeField] private Transform forwardTarget, backwardTarget;

    //private void Start()
    //{
    //    if (!agent) agent = gameObject.GetComponent<NavMeshAgent>();
    //    if (agent)
    //    {
    //        agent.updateRotation = false;
    //        agent.updateUpAxis = false;
    //    }
    //    isAttack = gameObject.GetComponentInChildren<AttackController>();
    //}
    //public void ДатьВЖбан(Entity e)
    //{
    //    Debug.Log("сущность");
    //    e.transform.position = (e.transform.position - transform.position) * physicalPower;
    //}
    //private void LateUpdate()
    //{
    //    animator.SetBool("isMove", isMove);
    //    animator.SetBool("isIdle", isIdle);
    //    animator.SetBool("isFightingStance", isFightingStance);
    //}

    //private void Update()
    //{
    //    Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector3 worldPos = new Vector3(wp.x, wp.y, 0);
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        isMove = true;
    //        isFightingStance = false;
    //        isIdle = false;
    //        animator.Play(animationPresset.Walk.name);
    //    }
    //    if (Input.GetKeyUp(KeyCode.W))
    //    {
    //        isMove = false;
    //        isIdle = true;
    //    }
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        agent.speed = forwardSpeed;
    //        agent.SetDestination(new Vector3(forwardTarget.position.x, forwardTarget.position.y, 0));
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        agent.speed = backwardSpeed;
    //        agent.SetDestination(new Vector3(backwardTarget.position.x, backwardTarget.position.y, 0));
    //    }
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (!isAttack.IsAttack)
    //            if (isMove)
    //            {
    //                animator.Play(animationPresset.WalkFastAttacks[Random.Range(0, animationPresset.WalkFastAttacks.Count)].ClipName);
    //            }
    //            else if (isIdle)
    //            {
    //                animator.Play(animationPresset.FastAttacks[Random.Range(0, animationPresset.FastAttacks.Count)].ClipName);
    //            }
    //    }
    //    //if (Input.GetMouseButtonDown(1))
    //    //{
    //    //    if (!isAttack.IsAttack)
    //    //        if (isMove)
    //    //        {
    //    //            animator.Play(animationPresset.WalkPowerAttacks[Random.Range(0, animationPresset.WalkPowerAttacks.Count)].ClipName);
    //    //        }
    //    //        else if (isIdle)
    //    //        {
    //    //            animator.Play(animationPresset.PowerAttacks[Random.Range(0, animationPresset.PowerAttacks.Count)].ClipName);
    //    //        }
    //    //}
    //    GameUtils.LookAt2D(transform, wp, rotateOffset);
    //}
}
