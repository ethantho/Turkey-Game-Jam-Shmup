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
    public SpriteRenderer HitBoxIndicator;
    public GameObject Beam;
    
    
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


        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (bulletDelayCounter >= bulletDelay)
            {
                shootBullet();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Debug.Log("Trying to fire");
            if (canBeam())
            {
                //Debug.Log("CANBEAM PASSED");
                StartCoroutine(fireBeam()); 

            }
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

       

        if(bulletDelayCounter < bulletDelay)
        {
            bulletDelayCounter++;
        }


        
        
    }

    void shootBullet()
    {
        bulletDelayCounter = 0;
        Instantiate(Bullet, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        Instantiate(Bullet, RPod.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Instantiate(Bullet, LPod.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
    }

    void focusMode()
    {
        LPod.transform.position += new Vector3(1, 1, 0);
        RPod.transform.position += new Vector3(-1, 1, 0);
        focusing = true;
        HitBoxIndicator.enabled = true;

    }

    void nonFocusMode()
    {
        LPod.transform.position += new Vector3(-1, -1, 0);
        RPod.transform.position += new Vector3(1, -1, 0);
        focusing = false;
        HitBoxIndicator.enabled = false;
    }

    IEnumerator fireBeam()
    {
        Debug.Log("FIRING BEAM");
        yield return new WaitForSeconds(0.5f);//chargeup

        EffectManager.Start_CShake(0.5f, 0.3f);

        Beam.GetComponent<SpriteRenderer>().enabled = true;
        
        yield return new WaitForSeconds(0.75f);

        Beam.GetComponent<SpriteRenderer>().enabled = false;

    }

    bool canBeam()
    {
        return true;
        //TODO
    }
}
