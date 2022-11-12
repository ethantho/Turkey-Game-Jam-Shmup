using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEnemyAttack : MonoBehaviour
{
    //turn shooting on or off
    [SerializeField] bool enable;

    //for non-grounded enemies
    [SerializeField] bool dynamicEnemyTrigger;
    [SerializeField] GameObject[] toEnable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dynamicEnemyTrigger && collision.gameObject.tag == "Player")
        {
            foreach (GameObject enemy in toEnable)
            {
                ChangeEnemyState(enemy);
            }
        }
        else
        {
            ChangeEnemyState(collision.gameObject);
        }

    }

    void ChangeEnemyState(GameObject enemy)
    {

        if (enemy.GetComponent<EnemyShooter>() != null)
        {
            enemy.GetComponent<EnemyShooter>().enabled = enable;
            if (enable == false)
            {
                foreach (Transform child in enemy.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        if (enemy.GetComponent<Animator>() != null)
        {
            Debug.Log("activate animated enemy:" + enable.ToString());
            enemy.GetComponent<Animator>().SetBool("Activated", enable);
        }
    }
}
