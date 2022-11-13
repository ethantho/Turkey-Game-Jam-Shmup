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

        if (waitCounter <= 0)
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
            else
            {
                bullet = Instantiate(attack, transform.position, transform.rotation);
            }
            
            Vector2 directionToPlayer = (target.transform.position - transform.position).normalized; //+ new Vector3(0f, 5f, 0f)).normalized;

            //goes directly in red arrows direction
            if (shootDirectlyDown)
            {
                directionToPlayer = transform.right;

            }


            //shoot bullet
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (animator != null)
            {
                bullet.GetComponent<Rigidbody2D>().velocity = (directionToPlayer * speedOfAttack) + new Vector2(0, 5.5f);
            }
            else
            {
                bullet.GetComponent<Rigidbody2D>().velocity = (directionToPlayer * speedOfAttack);

            }

            yield return new WaitForSeconds(waitTimeBetweenEach);
        }
    }
}

