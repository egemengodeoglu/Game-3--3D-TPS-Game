//using MiracleWorks.Enums;
using UnityEngine;

namespace MiracleWorks.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
        void Die();
        bool IsDead { get; set; }
    }

    public interface IPushAble
    {
        void ApplyPushForce(Vector3 force);
    }


    public interface IReviveable
    {
        void Revive(int startHealth);
    }

    public interface IHealable
    {
        void Heal(int healAmount);
    }

    public interface IDamageDealer
    {
        void ApplyDamage(IDamageable obj, int damage);
    }

    public interface IThrowable
    {
        void Throw();
    }

    public interface IEquipable
    {
        void Equip();
        void UnEquip();
    }

    public interface IConsumable
    {
        void Consume();
    }

    public interface IWeapon
    {
        void Attack();
    }

    public interface IHitable
    {
        void Hit();
    }

    public interface ICollectiable
    {
        void Collect();
    }

    public interface IState
    {
        /*StateType TypeOfState();
        StateType ProcessTransitions();
        void Enter();
        StateType Update();
        void Exit();*/
    }

    public interface ICondition
    {
        bool IsMet();
    }

}