using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.JumpForce = StateMachine.Player.Data.AirData.JumpForce;
        StateMachine.Player.ForceReceiver.Jump(StateMachine.JumpForce);
        base.Enter();
        StartAnimation(StateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (StateMachine.Player.Controller.velocity.y <= 0)
        {
            StateMachine.ChangeState(StateMachine.FallState);
        }
    }
}
