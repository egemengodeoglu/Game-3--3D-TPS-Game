using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    public DieState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void OnStateEnter()
    {
    }

    public override void Tick()
    {
        if (stateMachine.enemy.IsDead)
        {
            stateMachine.StartCoroutine(DieWaiter());
        }
    }

    public IEnumerator DieWaiter()
    {
        stateMachine.enemy.IsDead = false;
        stateMachine.anim.SetInteger("EnemyAnim", 0);
        yield return new WaitForSeconds(stateMachine.enemy.animDieTime);
        stateMachine.enemy.Die();

    }

    public override void OnStateExit()
    {

    }
}
