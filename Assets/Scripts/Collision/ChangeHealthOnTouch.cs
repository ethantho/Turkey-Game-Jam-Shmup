using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealthOnTouch : MonoBehaviour
{
    //MAKE NEGATIVE IF DECREASE HEALTH
    [SerializeField] int changeHealthBy;
    [SerializeField] GameObject explosionBullet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        explosionBullet = EffectManager.instance.bulletPref;
        if (collision.gameObject.tag != gameObject.tag)
        {
            if (collision.gameObject.GetComponent<hasHealth>())
            {
                collision.gameObject.GetComponent<hasHealth>().ChangeHealth(changeHealthBy);
                EffectManager.Start_Flicker(collision.gameObject.GetComponent<SpriteRenderer>(),2f);
            }

            //destroy no matter what
            Instantiate(explosionBullet,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
