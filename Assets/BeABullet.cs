using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeABullet : MonoBehaviour
{
    public int bulletDeathCounter = 0;
    public int bulletDeathFrames = 120;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, 1f, 0);
        bulletDeathCounter++;

        if (bulletDeathCounter > bulletDeathFrames)
        {
            Destroy(this.gameObject);
        }
    }
}