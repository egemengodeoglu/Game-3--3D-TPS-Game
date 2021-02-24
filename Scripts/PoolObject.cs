using System;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    /// <summary>
    /// Referance To Prefab Id Used In PoolManager As Key Value
    /// </summary>
    [HideInInspector]
    public int poolid;
    /// <summary>
    /// OnHideObject is The Trigger To Alert PoolManager To Disable Object And Return To Queue
    /// </summary>
    public Action<PoolObject> OnHideObject;

    /// <summary>
    /// If Set To True Object is Hidden After Delay. Can Be Used For Pooled Particle Effects
    /// </summary>
    public bool isHideOnDelay = false;
    public bool destroyOnNoPool = true;

    public float hideDelay = 1f;
    protected float _timer = 0f;

    protected virtual void OnEnable()
    {
        _timer = 0;
    }

    protected virtual void Update()
    {
        if (isHideOnDelay)
        {
            _timer += Time.deltaTime;
            if (_timer > hideDelay)
            {
                HideObject();
            }
        }
    }

    protected void HideObject()
    {
        if (OnHideObject != null)
        {
            OnHideObject.Invoke(this);
            return;
        }
        else if (destroyOnNoPool)
        {
            Destroy(this.gameObject);
        }

    }
}