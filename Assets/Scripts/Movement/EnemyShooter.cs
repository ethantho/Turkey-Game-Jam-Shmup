using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject attack;
    [SerializeField] float waitTimeBetweenBurst;
    [SerializeField] float waitTimeBetweenEach;
    [SerializeField] float speedOfAttack;
    [SerializeField] int burstNum;
    [SerializeField] bool shootDirectlyDown; //directly left, right, up, or down
    [SerializeField] bool readyToShoot = true;


    [SerializeField] bool partOfAnimatedUnit; //FOR BOSSES ROTATE GAMEOBJECT

    Animator animator;
    float waitCounter;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        waitCounter = 0;

        //false until entered trigger and turned on
        GetComponent<EnemyShooter>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        waitCounter -= Time.deltaTime;

        //turn off shooting if we are in default stage
        if(animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Finished"))
        {
            GetComponent<EnemyShooter>().enabled = false;
        }

        if (waitCounter <= 0 && readyToShoot)
        {
            waitCounter = waitTimeBetweenBurst;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    { 
        for (int i = 0; i < burstNum; i++)
        {
            GameObject bullet;
            //grounded enemies child it
            if (animator == null)
            {
                bullet = Instantiate(attack, transform);
            }
            else if (partOfAnimatedUnit)
            {
                bullet = Instantiate(attack, transform.parent.parent);
                bullet.transform.position = transform.position;
            }
            else
            {
                //bullet = Instantiate(attack, transform.position, transform.rotation);

                //attach to spawner
                bullet = Instantiate(attack, transform.parent);
                bullet.transform.position = transform.position;
            }
            
            Vector2 directionToPlayer = (target.transform.position - transform.position).normalized; //+ new Vector3(0f, 5f, 0f)).normalized;

            //goes directly in red arrows direction
            if (shootDirectlyDown)
            {
                directionToPlayer = transform.right;

            }


            //shoot bullet
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            bullet.GetComponent<Rigidbody2D>().velocity = (directionToPlayer * speedOfAttack);
            /*
            if (animator != null)
            {
                bullet.GetComponent<Rigidbody2D>().velocity = (directionToPlayer * speedOfAttack) + new Vector2(0, 5.5f);
            }
            else
            {


            }*/

            yield return new WaitForSeconds(waitTimeBetweenEach);
        }
    }
}

