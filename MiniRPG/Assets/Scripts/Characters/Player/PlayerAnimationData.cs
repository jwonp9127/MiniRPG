using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string runParameterName = "Run";
    
    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string jumpParameterName = "Jump";
    [SerializeField] private string fallParameterName = "Fall";
    
    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string comboAttackParameterName = "ComboAttack";

    [SerializeField] private string interactionParameterName = "@Interaction";
    [SerializeField] private string pickupParameter = "PickUp";
    [SerializeField] private string waveParameter = "Wave";
    
    public int GroundParameterHash { get; private set; } 
    public int IdleParameterHash { get; private set; } 
    public int WalkParameterHash { get; private set; } 
    public int RunParameterHash { get; private set; } 
    
    public int AirParameterHash { get; private set; } 
    public int JumpParameterHash { get; private set; } 
    public int FallParameterHash { get; private set; } 
    
    public int AttackParameterHash { get; private set; } 
    public int ComboAttackParameterHash { get; private set; } 
    
    public int InteractionParameterHash { get; private set; }
    public int PickUpParameterHash { get; private set; }
    public int WaveParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        
        AirParameterHash = Animator.StringToHash(airParameterName);
        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        FallParameterHash = Animator.StringToHash(fallParameterName);
        
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        ComboAttackParameterHash = Animator.StringToHash(comboAttackParameterName);
        
        InteractionParameterHash = Animator.StringToHash(interactionParameterName);
        PickUpParameterHash = Animator.StringToHash(pickupParameter);
        WaveParameterHash = Animator.StringToHash(waveParameter);
    }
}
