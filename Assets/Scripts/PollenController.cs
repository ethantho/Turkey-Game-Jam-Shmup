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
            Destroy(this.gameObject);

            //Player gets pollen
            collision.gameObject.GetComponent<hasScore>().Scored(numPoints);


            //play a subtle sound

        }
    }
}
