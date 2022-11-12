using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealthOnTouch : MonoBehaviour
{
    //MAKE NEGATIVE IF DECREASE HEALTH
    [SerializeField] int changeHealthBy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<hasHealth>())
        {
            collision.gameObject.GetComponent<hasHealth>().ChangeHealth(changeHealthBy);
        }
    }
}
