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

    public int invincibility = 70;
    public GameObject Bullet;
    public GameObject LPod;
    public GameObject RPod;
    public GameObject LGun;
    public GameObject RGun;
    bool focusing;
    public SpriteRenderer HitBoxIndicator;
    public GameObject Beam;
    public GameObject Beam2;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        invincibility--;
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
        //Instantiate(Bullet, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        Instantiate(Bullet, RGun.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Instantiate(Bullet, LGun.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Instantiate(Bullet, RPod.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Instantiate(Bullet, LPod.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        GetComponent<AudioSource>().Play();
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
        EffectManager.Start_Shake(GetComponent<collectsPollen>().pollenUI.transform.parent,5f);
        yield return new WaitForSeconds(0.05f);//chargeup
        GetComponent<collectsPollen>().UsePollen();
        //EffectManager.Start_CShake(0.5f, 0.3f);

        Beam.GetComponent<SpriteRenderer>().enabled = true;
        Beam.GetComponent<BoxCollider2D>().enabled = true;
        Beam2.GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(5f);

        Beam.GetComponent<SpriteRenderer>().enabled = false;
        Beam.GetComponent<BoxCollider2D>().enabled = false;
        Beam2.GetComponent<SpriteRenderer>().enabled = false;

    }

    bool canBeam()
    {
        return (GetComponent<collectsPollen>().CurrentPollen() >= 100);
        //TODO
    }
}
