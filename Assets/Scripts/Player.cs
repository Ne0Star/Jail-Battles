using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Entity
{
    [SerializeField] private float physicalPower;
    [SerializeField] private float forwardSpeed, backwardSpeed;
    [SerializeField] private float rotateOffset;
    [SerializeField] private bool isMove = false, isIdle = true, isFightingStance = false;
    [SerializeField] private Transform forwardTarget, backwardTarget;

    private void Start()
    {
        if (!agent) agent = gameObject.GetComponent<NavMeshAgent>();
        if (agent)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }
    [SerializeField] private float sd;
    private void Update()
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 worldPos = new Vector3(wp.x, wp.y, 0);
        if (Input.GetKeyDown(KeyCode.W))
        {
            isMove = true;
            isFightingStance = false;
            isIdle = false;
            //animator.Play(animationPresset.Walk.name);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            isMove = false;
            isIdle = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            agent.speed = forwardSpeed;
            agent.Move(transform.right * Time.fixedDeltaTime * sd * agent.speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            agent.speed = backwardSpeed;
            agent.Move(-transform.right * Time.fixedDeltaTime * sd * agent.speed);
        }
        if (Input.GetMouseButtonDown(0))
        {
            //if (!isAttack.IsAttack)
                if (isMove)
                {
                    //animator.Play(animationPresset.WalkFastAttacks[Random.Range(0, animationPresset.WalkFastAttacks.Count)].ClipName);
                }
                else if (isIdle)
                {
                    //animator.Play(animationPresset.FastAttacks[Random.Range(0, animationPresset.FastAttacks.Count)].ClipName);
                }
        }
        GameUtils.LookAt2D(transform, wp, rotateOffset);
    }
}
