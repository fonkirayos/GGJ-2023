using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovingState : CState<CPlayerController>
{
    public PMovingState(CStateMachine<CPlayerController> stateMachine) : base(stateMachine)    {    }

    public override void fixedUpdate(CPlayerController Controller)
    {

        Controller.Move(ref Controller.moveSpeed, ref Controller.maxVelocity);
        //Controller.rigidbody.velocity +=  new Vector2(Controller.speed * Time.fixedDeltaTime * Controller.horizontalAxis, 0f);

        //if (Controller.horizontalAxis > 0)
        //    Controller.spriteRenderer.flipX = false;
        //else
        //    Controller.spriteRenderer.flipX = true;



        //if (Mathf.Abs(Controller.rigidbody.velocity.x) > Controller.maxVelocity)
        //{
        //    if (Controller.rigidbody.velocity.x > 0) 
        //        Controller.rigidbody.velocity = new Vector2(Controller.maxVelocity, Controller.rigidbody.velocity.y);
        //    else
        //        Controller.rigidbody.velocity = new Vector2(-Controller.maxVelocity, Controller.rigidbody.velocity.y);
        //}
            
        //Debug.Log(Controller.rigidbody.velocity);
    }

    public override void onEnter(CPlayerController controller)
    {
        Debug.Log("Entering moving state");
        controller.bmoving = true;
    }

    public override void onExit(CPlayerController controller)
    {
        Debug.Log("Exiting moving state");
        controller.rigidbody.velocity = new Vector2(0f, controller.rigidbody.velocity.y);
        controller.bmoving = false;
    }

    public override void update(CPlayerController Controller)
    {
        inputManage(Controller);
    }

    protected override void inputManage(CPlayerController Controller)
    {
        Controller.horizontalAxis = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            Controller.stateMachine.setCurrentState(Controller.idleState, Controller);
        }
        if (Input.GetButtonUp("Jump") & !Controller.pauseCooldown)
        {
            if (Controller.jumpsLeft > 0)
            {
                Controller.stateMachine.setCurrentState(Controller.jumpingState, Controller);
            }
        }
    }
}
