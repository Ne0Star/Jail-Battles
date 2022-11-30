using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_1 : PlayerController
{
    [SerializeField] private Joystick controller;
    [SerializeField] private Player player;
    [SerializeField] private Stat moveSpeed;
    public Controller_1(Player player,ref Stat moveSpeed, Joystick controller)
    {
        this.player = player;
        this.moveSpeed = moveSpeed;
        this.controller = controller;
    }

    [SerializeField] private bool isWalk, isIdle, rotated, dragabble;
    [SerializeField] private Vector3 lastPos;
    public override void Update()
    {


        if (!rotated)
        {
            moveSpeed.DeBuffToPercent(40f);
        }
        else
        {
            moveSpeed.Normalize();
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragabble = false;
            lastPos = (player.Agent.transform.position + (player.Agent.velocity * -1));
        } else if (Input.GetMouseButton(0))
        {
            dragabble=true;
            lastPos = (player.Agent.transform.position + (player.Agent.velocity * -1));
        }


        if (Vector2.Distance(new Vector2(Mathf.Abs(controller.Direction.x), Mathf.Abs(controller.Direction.y)), Vector2.zero) > 0.1f)
        {
            if (!isWalk)
            {
                player.Animator.Play("walk");
                isWalk = true;
            }
            isIdle = false;
        }
        else
        {
            if (!isIdle)
            {
                player.Animator.Play("fightStance");
                isIdle = true;
            }
            isWalk = false;
        }
        //Debug.Log(controller.Direction);

        player.Agent.Move(controller.Direction.normalized * moveSpeed.CurrentValue * Time.deltaTime);
        if(dragabble)
        GameUtils.LookAt2DSmooth(player.RotateParent, lastPos, player.RotateOffset, Time.deltaTime * (player.Agent.speed * LevelManager.Instance.LevelData.RotateMultipler), 0.01f, () =>
        {
            rotated = true;
        });
    }
}
