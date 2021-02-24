using MiracleWorks.Interfaces;
using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : PoolObject, IDamageable
{
    public enum EnemyType
    {
        RangedAttacker,
        MeleeAttacker,
        MagicAttacker
    }

    public EnemyType enemyType;

    [Tooltip("Max Healt!")]
    public float maxHealth = 100;
    public PoolBullet poolBullet;
    public Transform bulletSpawnTransform;
    public GameObject weapon;
    public float f_RotSpeed;
    public float attackDistance;
    public float animShootTime,animDieTime;



    public float turnSmoothTime = 0.1f, turnSmoothVelocity;

    [HideInInspector]
    public bool isAttacking, isAttackFinished;
    public bool isDie;
    private float health;
    private NavMeshAgent agent;
    private GameObject player;

    protected override void OnEnable()
    {
        if (enemyType == EnemyType.MeleeAttacker)
        {
            weapon.GetComponent<Collider>().enabled = false;
        }
        this.GetComponent<Collider>().enabled = true;
        IsDead = false;
        base.OnEnable();
        health = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    
    public bool IsLookingToPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        float degree = Vector3.Angle( transform.forward, direction);
        if (Math.Abs(degree) >= 3)
        {
            this.transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    f_RotSpeed * Time.deltaTime);
            return false;
        }
        else
        {
            return true;
        }
        
    }
    public void DontFollowPlayer()
    {
        agent.SetDestination(transform.position);
    }
    public void FollowPlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    public bool IsArrivedToPlayer(float tmp)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= tmp;
    }

    public void Die()
    {
        health = maxHealth;
        OnHideObject?.Invoke(this);

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            IsDead = true;
        }
    }

    public void DieStarter()
    {
        this.GetComponent<Collider>().enabled = false;
    }

    public void SendArrow()
    {
        PoolManager.Instance.UseObject(poolBullet, bulletSpawnTransform.position, transform.rotation);
    }

    public bool IsDead
    {
        get
        {
            return isDie;
        }
        set
        {
            isDie = value;
        }
    }

    public void AttackIsFinished()
    {
        isAttacking = false;
        isAttackFinished = true;
    }

}
