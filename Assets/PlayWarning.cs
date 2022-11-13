using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWarning : MonoBehaviour
{
    public SpriteRenderer warningSign;
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
        GetComponent<AudioSource>().Play();
        warningSign.enabled = true;
        EffectManager.Start_Glitch(warningSign, 1);
    }
}
