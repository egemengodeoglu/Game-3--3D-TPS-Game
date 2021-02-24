using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;
    public RangeAttackState rangeAttackState;
    public IdleState idleState;
    public WanderState wanderState;
    public DieState dieState;
    [HideInInspector]
    public Enemy enemy;
    
    [HideInInspector]
    public Animator anim; //0-> Die Anim    1-> Run Anim    2-> Attack Anim    3->Idle Anim
    [HideInInspector]
    public bool isWaiting;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
        idleState = new IdleState(this);
        wanderState = new WanderState(this);
        rangeAttackState = new RangeAttackState(this);
        dieState = new DieState(this);
        isWaiting = false;
    }
    
    void OnEnable()
    {
        SetState(idleState);
    }

    void Update()
    {
        currentState.Tick();
        if (enemy.IsDead)
        {
            SetState(dieState);
        }
    }

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;

        if (currentState != null)
            currentState.OnStateEnter();
    }

    public IEnumerator Waiter(float time)
    {
        isWaiting = false;
        yield return new WaitForSeconds(time);
        isWaiting = true;
    }
}
