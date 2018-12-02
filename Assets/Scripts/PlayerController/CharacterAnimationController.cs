using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private const string kParamSpeedFloat = "Speed";
    private const string kParamJumpTrigger = "Jump";
    private const string kParamFallingBool = "Falling";
    private const string kParamAttackTrigger = "Attack";
    private const string kParamDieTrigger = "Die";
    private const string kParamHurtTrigger = "Hurt";
    private const string kParamGroundedTrigger = "Grounded";


    [SerializeField] private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void CharacterSpeed(float speed)
    {
        _animator.SetFloat(kParamSpeedFloat, speed);
    }

    public void CharacterJump()
    {
        _animator.SetTrigger(kParamJumpTrigger);
    }

    public void CharacterFalls(bool value)
    {
        _animator.SetBool(kParamFallingBool, value);
    }

    public void CharacterAttack()
    {
        _animator.SetTrigger(kParamAttackTrigger);
    }

    public void CharacterDie()
    {
        _animator.SetTrigger(kParamDieTrigger);
    }

    public void CharacterHurt()
    {
        _animator.SetTrigger(kParamHurtTrigger);
    }
    
    public void CharacterGrounded()
    {
        _animator.SetTrigger(kParamGroundedTrigger);
    }
}