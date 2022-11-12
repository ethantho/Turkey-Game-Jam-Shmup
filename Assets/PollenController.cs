using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenController : MonoBehaviour
{
    //public Collider2D PlayerPickupCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGER ENTERED");
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("ITS THE PLAYER");
            Destroy(this.gameObject);
            //Player gets pollen
            //play a subtle sound
        }
    }
}
