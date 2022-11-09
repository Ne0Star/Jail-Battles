using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Entity
{
    [SerializeField] private Weapon gun, machine;
    [SerializeField] private float rotateOffset;
    [SerializeField] private Stat moveSpeed;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform rotateParent;
    
    public Transform RotateParent { get => rotateParent; }
    public float RotateOffset { get => rotateOffset; }
    public PlayerController PlayerController { get => playerController; }

    private void Awake()
    {
        playerController = new Controller_0(this, moveSpeed);
    }

    private void Start()
    {
        animator.Play("fightStance");

    }

    private void Update()
    {
        playerController.Update();
    }





    //[SerializeField] Stat moveSpeed;
    //[SerializeField] private float physicalPower;
    //[SerializeField] private float forwardSpeed, backwardSpeed;
    //[SerializeField] private float rotateOffset;

    //[SerializeField] private float rotateSlowing;

    //[SerializeField] private bool isMove = false, isIdle = true, isFightingStance = false;
    //[SerializeField] private Transform forwardTarget, backwardTarget, rotateParent;
    //private void Start()
    //{
    //    if (!agent) agent = gameObject.GetComponent<NavMeshAgent>();
    //    if (agent)
    //    {
    //        agent.updateRotation = false;
    //        agent.updateUpAxis = false;
    //    }
    //}
    //[SerializeField] private float sd;
    //[SerializeField] Vector3 test;
    //[SerializeField] private bool rotated = false;

    //Vector3 lastPos, currentPos;

    //private void Update()
    //{
    //    currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    if (!rotated)
    //    {
    //        moveSpeed.DeBuffToPercent(rotateSlowing);
    //    }
    //    else
    //    {
    //        moveSpeed.Normalize();
    //    }

    //    GameUtils.LookAt2DSmooth(rotateParent, currentPos, rotateOffset, Time.unscaledDeltaTime * (agent.speed * LevelManager.Instance.LevelData.RotateMultipler), 0.01f, () =>
    //    {
    //        rotated = true;
    //    });
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        isMove = true;
    //        Vector3 v = -(currentPos - agent.transform.position).normalized;
    //        //Vector3 move = new Vector3(v.x / 2, v.y);
    //        agent.Move(v * moveSpeed.CurrentValue * sd);
    //    }

    //    if(Input.GetKeyUp(KeyCode.W))
    //    {
    //        isMove = false;
    //        animator.Play("fightStance");
    //    }
    //    if(Input.GetKeyDown(KeyCode.W))
    //    {
    //        animator.Play("walk");
    //    }

    //    if(mouseMove != lastMouseMove)
    //    {
    //        if(mouseMove)
    //        {
    //            animator.Play("walk");
    //        } else
    //        {
    //            if(!isMove && allowRotate)
    //            animator.Play("fightStance");
    //        }
    //        lastMouseMove = mouseMove;
    //    }
    //    if(!isMove)
    //    {

    //        if(currentTime >= rotateBugTime)
    //        {
    //            allowRotate = !allowRotate;
    //            currentTime = 0f;
    //        }

    //        currentTime += Time.unscaledDeltaTime;
    //    }
    //}
    //[SerializeField] private float currentTime = 0f;
    //[SerializeField] private float rotateBugTime = 1f;
    //[SerializeField] bool mouseMove = false, lastMouseMove = false;
    //[SerializeField] private bool allowRotate = true;
    //private void FixedUpdate()
    //{
    //    rotated = false;
    //    if(lastPos != currentPos)
    //    {
    //        mouseMove = true;
    //        lastPos = currentPos;
    //    } else
    //    {
    //        mouseMove = false;
    //    }

    //}
}
