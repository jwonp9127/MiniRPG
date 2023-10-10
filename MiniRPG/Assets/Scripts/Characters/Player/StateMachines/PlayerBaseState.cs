using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine StateMachine;
    protected readonly PlayerGroundData GroundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        StateMachine = playerStateMachine;
        GroundData = StateMachine.Player.Data.GroundData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void Update()
    {
        Move();
    }

    public virtual void PhysicsUpdate()
    {
        
    }
    
    //
    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = StateMachine.Player.Input;
        input.PlayerActions.Movement.canceled += OnMovementCanceled;
        input.PlayerActions.Run.started += OnRunStarted;
        input.PlayerActions.Jump.started += OnJumpStarted;

        input.PlayerActions.Attack.performed += OnAttackPerformed;
        input.PlayerActions.Attack.canceled += OnAttackCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = StateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;        
        input.PlayerActions.Jump.started -= OnJumpStarted;
        
        input.PlayerActions.Attack.performed -= OnAttackPerformed;
        input.PlayerActions.Attack.canceled -= OnAttackCanceled;
    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnAttackPerformed(InputAction.CallbackContext context)
    {
        StateMachine.IsAttacking = true;
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        StateMachine.IsAttacking = false;
    }

    private void ReadMovementInput()
    {
        StateMachine.MovementInput = StateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        Rotate(movementDirection);
        Move(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 forward = StateMachine.MainCameraTransform.forward;
        Vector3 right = StateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * StateMachine.MovementInput.y + right * StateMachine.MovementInput.x;
    }

    private void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovementSpeed();
        StateMachine.Player.Controller.Move(
            (movementDirection * movementSpeed + StateMachine.Player.ForceReceiver.Movement) * Time.deltaTime);
    }

    protected void ForceMove()
    {
        StateMachine.Player.Controller.Move(StateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = StateMachine.MovementSpeed * StateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Transform playerTransform = StateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation,
                targetRotation, StateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected void StartAnimation(int animationHash)
    {
        StateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        StateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
