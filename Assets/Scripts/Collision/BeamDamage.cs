using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDamage : MonoBehaviour
{
    //MAKE NEGATIVE IF DECREASE HEALTH
    [SerializeField] int changeHealthBy;
    [SerializeField] float damageDelayCounter;// = 0.1f;
    [SerializeField] float damageDelay;// = 0.1f; 
    public GameObject Player;
    private void OnTriggerStay2D(Collider2D collision)
    {
        damageDelayCounter+= Time.deltaTime;
        if (collision.gameObject.GetComponent<hasHealth>())
        {
            float distance = Vector3.Distance(Player.transform.position, collision.gameObject.transform.position);
            if (damageDelayCounter >= damageDelay && distance <= 20)
            {
                collision.gameObject.GetComponent<hasHealth>().ChangeHealth(changeHealthBy);
                damageDelayCounter = 0;
            }
            
            //Destroy(gameObject);
        }
    }
}
