using MiracleWorks.Interfaces;
using UnityEngine;
using Time = UnityEngine.Time;

public class PoolBullet : PoolObject, IDamageDealer
{
    public float speed = 5;
    public RangedInteger damage;
    public bool applyPushForce = false;
    public float pushForce = 250f;
    private int maxReflect;

    public bool reflectOnCollision = false;

    public PoolObject hitEffect;

    [Tooltip("Define layers that projectile will collide")]
    public LayerMask hitMask;
    [Tooltip("Define layers that projectile will damage")]
    public LayerMask damageMask;

    [Tooltip("Collision radius of projectile")]
    [SerializeField]
    protected float radius = .15f;

    protected float deltaSpeed;

    protected Ray ray;
    protected RaycastHit hit;

    private void Awake()
    {
        ray = new Ray();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        deltaSpeed = speed * Time.fixedDeltaTime;
        maxReflect = 2;
    }

    protected void OnCollideWithEnemy()
    {
        PoolManager.Instance.UseObject(hitEffect, transform.position, Quaternion.identity);
            IDamageable hitObj = hit.collider.GetComponent<IDamageable>();
        if (hitObj != null)
        {
            ApplyDamage(hitObj, damage.RandomValue());
            //Debug.Log("Headshot attı!");
        }

            IPushAble pushable = hit.collider.GetComponent<IPushAble>();
            if (pushable != null && applyPushForce)
            {
                Vector3 direction = (hit.transform.position - transform.position).normalized;
                pushable.ApplyPushForce(direction * pushForce);
            }
        OnHideObject?.Invoke(this);
    }


    protected void FixedUpdate()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;

        //if (Physics.Raycast(ray, out hit, deltaSpeed, hitMask))
        if (Physics.SphereCast(ray, radius, out hit, deltaSpeed, hitMask))
        {
            transform.Translate(Vector3.forward * hit.distance);

            if ((damageMask.value >> hit.collider.gameObject.layer) == 1)
            {
                OnCollideWithEnemy();
            }
            else if (reflectOnCollision)
            {
                Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
                transform.rotation = Quaternion.LookRotation(reflectDir, Vector3.up);
                
                if (maxReflect > 0)
                {
                    maxReflect--;
                }
                else
                {
                    OnHideObject?.Invoke(this);
                }
            }
            else
            {
                PoolManager.Instance.UseObject(hitEffect, transform.position, Quaternion.identity);
                OnHideObject?.Invoke(this);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * deltaSpeed);
        }
    }

    public void ApplyDamage(IDamageable obj, int damage)
    {
        obj.TakeDamage(damage);
    }
}
