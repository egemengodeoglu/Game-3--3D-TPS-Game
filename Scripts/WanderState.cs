using UnityEngine;

public class WanderState : State
{
    public WanderState(StateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void OnStateEnter()
    {
        stateMachine.anim.SetInteger("EnemyAnim", 1);
    }

    public override void Tick()
    {
        stateMachine.enemy.FollowPlayer();
        if (stateMachine.enemy.IsArrivedToPlayer(stateMachine.enemy.attackDistance))
        {
            stateMachine.SetState(stateMachine.rangeAttackState);
        }
    }

    public override void OnStateExit()
    {
        stateMachine.enemy.DontFollowPlayer();
    }
}