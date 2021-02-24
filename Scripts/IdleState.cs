using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void OnStateEnter()
    {
        stateMachine.anim.SetInteger("EnemyAnim", 3);
        stateMachine.StartCoroutine(stateMachine.Waiter(stateMachine.enemy.animShootTime));
    }

    public override void Tick()
    {
        if (stateMachine.isWaiting)
        {
            if (stateMachine.enemy.IsArrivedToPlayer(stateMachine.enemy.attackDistance))
            {
                stateMachine.SetState(stateMachine.rangeAttackState);
            }
            else
            {
                stateMachine.SetState(stateMachine.wanderState);
            }
        }
    }

    public override void OnStateExit()
    {

    }
}
