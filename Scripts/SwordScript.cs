using JetBrains.Annotations;
using MiracleWorks.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour, IDamageDealer
{
    public void ApplyDamage(IDamageable obj, int damage)
    {
        obj.TakeDamage(damage);
    }
    

    public void OnTriggerEnter(Collider others)
    {
        if(others.gameObject.tag == "Player")
        {
            Debug.Log("Deneme:" + others.gameObject.tag.ToString());
            ApplyDamage(others.GetComponent<IDamageable>(), 45);
        }
    }
    

    /*
    private void OnTriggerEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            
            ApplyDamage(collision.gameObject.GetComponent<IDamageable>(), 45);
        }
    }*/
}