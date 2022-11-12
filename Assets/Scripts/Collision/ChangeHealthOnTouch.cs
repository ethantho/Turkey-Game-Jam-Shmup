using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealthOnTouch : MonoBehaviour
{
    //MAKE NEGATIVE IF DECREASE HEALTH
    [SerializeField] int changeHealthBy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<hasHealth>())
        {
            collision.gameObject.GetComponent<hasHealth>().ChangeHealth(changeHealthBy);
            Destroy(gameObject);
        }
    }
}
