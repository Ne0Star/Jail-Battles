using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller_0 : PlayerController
{
    [SerializeField] private Player player;
    [SerializeField] private Stat moveSpeed;
    public Controller_0(Player player, ref Stat moveSpeed)
    {
        this.player = player;
        this.moveSpeed = moveSpeed;
    }

    Vector3 currentPos;
    Vector3 v;
    [SerializeField] private bool rotated;
    public override void Update()
    {

        currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!rotated)
        {
            moveSpeed.DeBuffToPercent(40f);
        }
        else
        {
            moveSpeed.Normalize();
        }

        GameUtils.LookAt2DSmooth(player.RotateParent, currentPos, player.RotateOffset, Time.deltaTime * (player.Agent.speed * LevelManager.Instance.LevelData.RotateMultipler), 0.01f, () =>
        {
            rotated = true;
        });

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed.Buff(0.1f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            v = -(currentPos - player.Agent.transform.position).normalized;

            //Vector3 move = new Vector3(v.x / 2, v.y);
            player.Agent.Move(v * moveSpeed.CurrentValue * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            player.Animator.Play("fightStance");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.Animator.Play("walk");
        }
    }

    public override Vector3 Direction()
    {
        return v;
    }

}
