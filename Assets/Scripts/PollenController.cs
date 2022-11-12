using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenController : MonoBehaviour
{
    //collect pollen
    [SerializeField] int numPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGER ENTERED");
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("ITS THE PLAYER");
            collision.gameObject.GetComponent<PlayerController>().flourishMeter += 10;
            if(collision.gameObject.GetComponent<PlayerController>().flourishMeter > 100)
            {
                collision.gameObject.GetComponent<PlayerController>().flourishMeter = 100;
            }
            Destroy(this.gameObject);

            //Player gets pollen
            collision.gameObject.GetComponent<hasScore>().Scored(numPoints);


            //play a subtle sound
        }
    }
}
