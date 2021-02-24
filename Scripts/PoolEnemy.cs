/*using System;
using System.Collections;
using MiracleWorks.Core;
using MiracleWorks.Enums;
using MiracleWorks.FSM;
using UnityEngine;
using UnityEngine.AI;

public class PoolEnemy : PoolObject
{
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator     animator;
    [HideInInspector] public Health       health;
    [HideInInspector] public StateMachine stateMachine;

    public PoolObject bullet;

    public event Action<AnimationEventType> OnAnimationEvent;
    public Action<PoolEnemy> OnEnemyDie;
    public PoolObject        vanishEffect;

    [Header("Enable/Disable States")]
    [SerializeField] protected bool isUsingMeleeAttack  = true;
    [SerializeField] protected bool isUsingRangedAttack = true;
    [SerializeField] protected bool isUsingSkillAttack  = true;

    public bool IsUsingMeleeAttack { get { return isUsingMeleeAttack; } }
    public bool IsUsingRangedAttack { get { return isUsingMeleeAttack; } }
    public bool IsUsingSkillAttack { get { return isUsingMeleeAttack; } }

    public bool canMeleeAttack  = true;
    public bool canRangedAttack = true;
    public bool canSkillAttack  = false;

    [SerializeField] protected float meleeAttackTime  = 3;
    [SerializeField] protected float rangedAttackTime = 3;
    [SerializeField] protected float skillAttackTime  = 4;

    void Awake()
    {
        health       = GetComponent<Health>();
        stateMachine = GetComponent<StateMachine>();
        agent        = GetComponent<NavMeshAgent>();
        animator     = GetComponentInChildren<Animator>();
    }

    public IEnumerator SetMeleeAttackCooldown()
    {
        canMeleeAttack = false;
        yield return new WaitForSeconds(meleeAttackTime);
        canMeleeAttack = true;
    }

    public IEnumerator SetRangedAttackCooldown()
    {
        canRangedAttack = false;
        yield return new WaitForSeconds(rangedAttackTime);
        canRangedAttack = true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        health.ResetHealth();
        health.onDie += OnDie;
    }

    protected void OnDisable()
    {
        StopAllCoroutines();
        canMeleeAttack = canRangedAttack = canSkillAttack = true;
        health.onDie -= OnDie;
    }

    private void OnDie()
    {
        OnEnemyDie?.Invoke(this);
        stateMachine.ChangeState(StateType.Die);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }

    public void AnimationEvent(AnimationEventType eventType)
    {
        OnAnimationEvent?.Invoke(eventType);
    }

}
*/