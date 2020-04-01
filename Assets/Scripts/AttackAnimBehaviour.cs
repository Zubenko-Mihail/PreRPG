using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AttackAnimBehaviour : StateMachineBehaviour
{
    public float attackSpeed;
    public Attack attackScript;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackScript.AttackAnimBeginning();
        animator.SetFloat("AttackSpeed", stateInfo.length * attackSpeed);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackScript.AttackAnimEnded();
        animator.SetFloat("AttackSpeed", 1);
    }
}
