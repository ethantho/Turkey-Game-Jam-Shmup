using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeABullet : MonoBehaviour
{
    public int bulletDeathCounter = 0;
    public int bulletDeathFrames = 120;
    public int bulletSpeedMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        bulletDeathCounter++;

        if (bulletDeathCounter > bulletDeathFrames)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * bulletSpeedMultiplier;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IsBorder>() != null)
        {
            Destroy(this.gameObject);
        }
    }
}
