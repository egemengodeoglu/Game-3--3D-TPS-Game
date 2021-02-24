using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : State
{
    public RangeAttackState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnStateEnter()
    {
        stateMachine.anim.SetInteger("EnemyAnim", 3);
        stateMachine.enemy.isAttacking = false;
        stateMachine.enemy.isAttackFinished = false;
    }

    public override void Tick()
    {
        if (!stateMachine.enemy.isAttacking)
        {
            if (!stateMachine.enemy.IsArrivedToPlayer(stateMachine.enemy.attackDistance+1))
            {
                stateMachine.SetState(stateMachine.wanderState);
            }
            else
            {
                if (stateMachine.enemy.IsLookingToPlayer())
                {
                    stateMachine.enemy.isAttacking = true;
                    stateMachine.anim.SetInteger("EnemyAnim", 2);
                }
            }
        }
        if (stateMachine.enemy.isAttackFinished)
        {
            stateMachine.StartCoroutine(shootWaiter());
        }
    }
    public IEnumerator shootWaiter()
    {
        stateMachine.enemy.isAttackFinished = false;
        stateMachine.anim.SetInteger("EnemyAnim", 3);
        yield return new WaitForSeconds(5f);
        stateMachine.SetState(stateMachine.idleState);
    }


    public override void OnStateExit()
    {
    }
}
