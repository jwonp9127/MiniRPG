using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Player.AnimationData.FallParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (StateMachine.Player.Controller.isGrounded)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }
}
