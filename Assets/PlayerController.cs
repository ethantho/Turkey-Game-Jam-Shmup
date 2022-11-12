using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float baseSpeed;
    public float focusSpeed;
    private float speed;
    public int bulletDelay = 6;
    public int bulletDelayCounter = 0;
    public GameObject Bullet;
    public GameObject LPod;
    public GameObject RPod;
    bool focusing;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            focusMode();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            nonFocusMode();
        }


   
    }


    void FixedUpdate()
    {
        if (!focusing)
        {
            speed = baseSpeed;
        }
        else
        {
            speed = focusSpeed;
        }
        
        if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            speed /= Mathf.Sqrt(2);
        }
        //rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0));
        //rb.AddForce(new Vector2(0, Input.GetAxis("Vertical") * speed));
        transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed, 0);

        if (Input.GetKey(KeyCode.Space))
        {
            if(bulletDelayCounter >= bulletDelay)
            {
                shootBullet();
            }
        }

        if(bulletDelayCounter < bulletDelay)
        {
            bulletDelayCounter++;
        }


        
        
    }

    void shootBullet()
    {
        bulletDelayCounter = 0;
        Instantiate(Bullet, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        Instantiate(Bullet, RPod.transform.position, Quaternion.identity);
        Instantiate(Bullet, LPod.transform.position, Quaternion.identity);
    }

    void focusMode()
    {
        LPod.transform.position += new Vector3(1, 1, 0);
        RPod.transform.position += new Vector3(-1, 1, 0);
        focusing = true;
    }

    void nonFocusMode()
    {
        LPod.transform.position += new Vector3(-1, -1, 0);
        RPod.transform.position += new Vector3(1, -1, 0);
        focusing = false;
    }
}
