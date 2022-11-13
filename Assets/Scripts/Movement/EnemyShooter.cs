using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject attack;
    [SerializeField] float waitTimeBetween;
    [SerializeField] float speedOfAttack;
    [SerializeField] int burstNum;
    [SerializeField] bool gridAim; //directly left, right, up, or down

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
            waitCounter = waitTimeBetween;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    { 
        for (int i = 0; i < burstNum; i++)
        {
            GameObject bullet = Instantiate(attack, transform);
            Vector2 directionToPlayer = target.transform.position - transform.position;

            //if going directly left, right, up, or down
            if (gridAim)
            {
                if (Mathf.Abs(directionToPlayer.y) > Mathf.Abs(directionToPlayer.x))
                {
                    //directionToPlayer = directionToPlayer.normalized;
                    directionToPlayer = new Vector3(0, directionToPlayer.y, 0);
                }
                else
                {
                    //directionToPlayer = directionToPlayer.normalized;
                    directionToPlayer = new Vector3(directionToPlayer.x, 0, 0);
                    //rotate to face player
                    bullet.transform.eulerAngles = (new Vector3(0, 0, 90));
                }
            }
            else
            {
                //rotate to face player
                Vector3 original = bullet.transform.eulerAngles;
                bullet.transform.LookAt(target.transform);
                bullet.transform.eulerAngles = new Vector3(0, 0, bullet.transform.eulerAngles.x + 90f);
            }

            //shoot bullet
            bullet.GetComponent<Rigidbody2D>().AddForce(directionToPlayer * speedOfAttack);

            yield return new WaitForSeconds(0.2f);
        }
    }
}