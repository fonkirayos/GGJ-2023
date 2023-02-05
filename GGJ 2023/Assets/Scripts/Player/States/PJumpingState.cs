using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PJumpingState : CState<CPlayerController>
{
    public PJumpingState(CStateMachine<CPlayerController> stateMachine) : base(stateMachine)    {  }

    public override void fixedUpdate(CPlayerController Controller)
    {
        if (Controller.jumpPending & !Controller.pauseCooldown)
        {
            Controller.Jump();
            Controller.jumpPending = false;
        }

        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            Controller.bmoving = true;
            Controller.Move(ref Controller.moveJumpSpeed, ref Controller.maxVelocityJump);
        }
        
        if(Controller.rigidbody.velocity.y == 0f & Controller.bFloor)
        {
            Controller.stateMachine.setCurrentState(Controller.idleState, Controller);
        }
    }

    public override void onEnter(CPlayerController controller)
    {
        controller.bjumping = true;
        controller.jumpsLeft--;
        controller.rigidbody.velocity = new Vector2(controller.rigidbody.velocity.x, 1f);
        controller.jumpPending = true;
    }

    public override void onExit(CPlayerController controller)
    {
        controller.bjumping = false;
        controller.bmoving = false;
    }

    public override void update(CPlayerController Controller)
    {
        inputManage(Controller);
    }

    protected override void inputManage(CPlayerController Controller)
    {
        Controller.horizontalAxis = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            Controller.bmoving = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0f)
        {
            Controller.bmoving = false;
        }
        if (Input.GetButtonUp("Jump") & !Controller.pauseCooldown)
        {
            if(Controller.jumpsLeft > 0)
            {
                Controller.Jump();
            }
        }
    }
}
