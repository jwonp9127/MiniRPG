using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedForce;
    private bool alreadyApplyCombo;

    AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.Player.AnimationData.ComboAttackParameterHash);

        alreadyAppliedForce = false;
        alreadyApplyCombo = false;

        int comboIndex = StateMachine.ComboIndex;
        attackInfoData = StateMachine.Player.Data.AttackData.GetAttackInfo(comboIndex);
        StateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Player.AnimationData.ComboAttackParameterHash);

        if (!alreadyApplyCombo)
        {
            StateMachine.ComboIndex = 0;
        }
    }
    
    public override void Update()
    {
        base.Update();
        ForceMove();
        float normalizedTime = GetNormalizedTime(StateMachine.Player.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
            {
                TryApplyForce();
            }

            if (normalizedTime >= attackInfoData.ComboTransitionTime)
            {
                TryComboAttack();
            }
        }
        else
        {
            if (alreadyApplyCombo)
            {
                StateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                StateMachine.ChangeState(StateMachine.ComboAttackState);
            }
            else
            {
                StateMachine.ChangeState(StateMachine.IdleState);
            }
        }
    }

    private void TryComboAttack()
    {
        if (alreadyApplyCombo) return;
        if (attackInfoData.ComboStateIndex == -1) return;
        if (!StateMachine.IsAttacking) return;

        alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        StateMachine.Player.ForceReceiver.Reset();
        StateMachine.Player.ForceReceiver.AddForce(StateMachine.Player.transform.forward * attackInfoData.Force);
    }
}
