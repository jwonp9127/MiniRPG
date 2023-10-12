using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
        
        // if(StateMachine.IsAttacking)
        // {
        //     OnAttack();
        //     return;
        // }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!StateMachine.Player.Controller.isGrounded
            && StateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.deltaTime + StateMachine.Player.DropMargin)
        {
            StateMachine.ChangeState(StateMachine.FallState);
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (StateMachine.MovementInput == Vector2.zero)
        {
            return;
        }
        StateMachine.ChangeState(StateMachine.IdleState);
        
        base.OnMovementCanceled(context);
    }

    protected virtual void OnMove()
    {
        StateMachine.ChangeState(StateMachine.WalkState);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        StateMachine.ChangeState(StateMachine.JumpState);
    }

    protected virtual void OnAttack()
    {
        StateMachine.ChangeState(StateMachine.ComboAttackState);
    }
}

