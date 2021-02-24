using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RayCast_Try1 : MonoBehaviour
{
    RaycastHit fire;
    public PoolObject poolBullet;

    private Rigidbody rb;
    public float verticalSpeed, horizontalSpeed, boostSpeed, currentspeed; //2,2,5,0

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 40, Color.green);
    }

    void FixedUpdate()
    {/*
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // float dr is declared outside
        There is no diagonal rotation, atm. 
        if (h != 0) dr = 90 * h;
        if (v < 0) dr = 180;
        else if (v > 0) dr = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, dr, 0), 360 * Time.deltaTime);
        // not normalized, yet.
        transform.position += new Vector3(h, 0, v) * Time.deltaTime * 2f;
        */

      
        float moveHorizontal = horizontalSpeed * Input.GetAxis("Horizontal");
        float moveVertical = verticalSpeed * Input.GetAxis("Vertical");
        currentspeed = rb.velocity.magnitude;
        if(Input.GetKey("left shift"))
        {
            transform.position += new Vector3(moveHorizontal, 0f, moveVertical) * Time.deltaTime * boostSpeed;
            rb.rotation = Quaternion.Euler(0, moveHorizontal*-90, 0);
            //new Vector3(moveHorizontal, 0.0f, 2 * boostSpeed * moveVertical);
        }
        else
        {
            rb.rotation = Quaternion.Euler(0, moveHorizontal*-90, 0);
            transform.position += new Vector3(moveHorizontal, 0f, moveVertical) * Time.deltaTime;
            //movemont = new Vector3(moveHorizontal, 0.0f, 2 * moveVertical);
        }

        //rb.velocity = movemont;

    }


    public void Fire()
    {
        Vector3 deneme;
        deneme = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Debug.Log("Fire");
        if (Physics.Raycast(deneme, transform.forward, out fire,80))
        {
            PoolManager.Instance.UseObject(poolBullet, transform.position, Quaternion.identity);
            if(fire.collider.name == "Enemy")
            {
                Debug.Log("You hit");
            }
            else
            {
                Debug.Log("You missed");
            }

            /*
            GameObject temporaryBullet;
            temporaryBullet = Instantiate(bullet, deneme, transform.rotation) as GameObject;
            temporaryBullet.GetComponent<Rigidbody>().velocity = (fire.point - deneme).normalized * 1;
            */
        }
        
    }

}
