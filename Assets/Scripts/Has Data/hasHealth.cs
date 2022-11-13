using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hasHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private healthUI UI;
    [SerializeField] GameObject PollenPickup;
    [SerializeField] GameObject explosionEnemy;
    [SerializeField] GameObject explosionBig;
    [SerializeField] bool hasHealthUI;

    private bool isPlayer;
    int currentHealth;
    Vector3 startingLocalPos;

    bool dead; //for dealing with playing animation then destroying

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        startingLocalPos = transform.localPosition;
        explosionEnemy = EffectManager.instance.explosionPref;
        explosionBig = EffectManager.instance.bigExplosionPref;

        if (GetComponent<PlayerController>() != null)
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }

        if (hasHealthUI)
        {
            UI.SetUI(maxHealth);
        }

        dead = false;
    }

    private void Update()
    {
        if (dead)
        {
            if (isPlayer)
            {
                transform.localPosition = startingLocalPos;
                GetComponent<Animator>().Play("Default");
                //todo decrement life
            }
            else
            {
                Instantiate(explosionEnemy,transform.position,transform.rotation);
                Instantiate(PollenPickup, transform.position, transform.rotation);
                GetComponent<AudioSource>().Play();
                //gameObject.SetActive(false);


                if (hasHealthUI)//was a boss kill homies
                {
                    if (transform.parent.gameObject.GetComponent<BossController>() != null)
                    {
                        transform.parent.gameObject.GetComponent<BossController>().enabled = false;
                    }

                    foreach (Transform child in transform.parent)
                    {
                        if (child.gameObject != gameObject)//dont wanna delete self until end
                        {
                            Destroy(child.gameObject);
                        }
                    }
                }

                Destroy(gameObject);

            }
        }
    }


    public void ChangeHealth(int amount)
    {
        if (!dead)
        {
            //update health
            if(!isPlayer){
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            }
            else if(GetComponent<PlayerController>().invincibility<=0){
                currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
                    //EffectManager.Start_CShake(.5f, .5f);
                GetComponent<PlayerController>().invincibility=70;
            }
                
            //update UI
            if (hasHealthUI)
            {
                UI.UpdateUI(currentHealth);
            }

            //death
            if (currentHealth <= 0)
            {
                if (isPlayer)
                {
                    dead = true;
                }
                else
                {
                    if (GetComponent<EnemyShooter>() != null)
                    {
                        GetComponent<EnemyShooter>().enabled = false;
                    }

                    if (GetComponent<AudioSource>()!= null){
                        GetComponent<AudioSource>().Play();
                    }
                    if (hasHealthUI)//was a boss kill homies
                    {
                        EffectManager.Start_BigExplosion(transform.position.x,transform.position.y);
                    }


                    dead = true;

                    //play animation
                }
            }
        }
    }

}
