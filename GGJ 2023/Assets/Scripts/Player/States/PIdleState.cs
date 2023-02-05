using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIdleState : CState<CPlayerController>
{
    public PIdleState(CStateMachine<CPlayerController> stateMachine) : base(stateMachine)    {    }

    public override void fixedUpdate(CPlayerController Controller)
    {
       // Controller.rigidbody.velocity = new Vector2(Controller.speed * Time.fixedDeltaTime, 0f);
    }

    public override void onEnter(CPlayerController controller)
    {
        
    }

    public override void onExit(CPlayerController controller)
    {
        controller.bidle = false;
    }

    public override void update(CPlayerController Controller)
    {
        Controller.bidle = true;
        inputManage(Controller);
    }

    protected override void inputManage(CPlayerController Controller)
    {
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        if(Input.GetAxisRaw("Horizontal") != 0f)
        {
            Controller.stateMachine.setCurrentState(Controller.movingState, Controller);
        }
        if (Input.GetButtonUp("Jump") & !Controller.pauseCooldown)
        {
            if(Controller.jumpsLeft > 0)
            {
                Controller.stateMachine.setCurrentState(Controller.jumpingState, Controller);
            }
            
        }
    }
}
