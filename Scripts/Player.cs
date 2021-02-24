using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MiracleWorks.Interfaces;

public class Player : MonoBehaviour, IDamageable
{
    public float speed = 6f, speedBooster = 1000f, turnSmoothTime = 0.1f, turnSmoothVelocity, shootTime;
    public PoolObject poolBullet;
    public Transform staffTarget;
    [Tooltip("Max Healt!")] public float health = 100;

    private Animator anim;
    private bool isRunning, isShoot, isDie, isLooking;
    //private CharacterController characterController;

    void Start()
    {
        //characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        anim.SetInteger("playerAnim",1);
        isRunning = false;
        isShoot = false;
        isDie = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        float canLook = VirtualJoyStick.Instance.Direction.magnitude;
        if (canLook >= 0.1f)
        {
            isLooking = true;
            transform.LookAt(VirtualJoyStick.Instance.Direction * 360);
            if (canLook >= 0.4f & !isShoot)
            {
                StartCoroutine(Shooting());
            }
            
        }
        else
        {
            isLooking = false;
        }
  
        if(direction.magnitude >= 0.1f)
        {
            isRunning = true;
            transform.position += direction*Time.deltaTime*speed;
            if (!isLooking)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
        else
        {
            isRunning = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Shifter();
        }
        AnimatorContollerFunction();
    }

    public void Shifter()
    {
        Debug.Log("Shifter is worked");
        Vector3 moveDir = Quaternion.Euler(0f,transform.rotation.eulerAngles.y,0f) * Vector3.forward;
        //characterController.Move(moveDir.normalized * speedBooster * Time.deltaTime);
    }

    private IEnumerator Shooting()
    {
        isShoot = true;
        VirtualJoyStick.Instance.ShootPlayer();
        anim.SetInteger("playerAnim", 2);
        yield return new WaitForSeconds(shootTime);
        anim.SetInteger("playerAnim", 0);
        isShoot = false;
    }

    public void AnimatorContollerFunction()
    {
        if (!isShoot)
        {
            if (isRunning)
            {
                anim.SetInteger("playerAnim", 1);
            }
            else
            {
                anim.SetInteger("playerAnim", 0);
            }
        }
    }

    public void FireBullet()
    {
        PoolManager.Instance.UseObject(poolBullet, staffTarget.transform.position, transform.rotation);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            IsDead = true;
            Debug.Log("Player is dead!");
        }
    }

    public void Die()
    {

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

}
