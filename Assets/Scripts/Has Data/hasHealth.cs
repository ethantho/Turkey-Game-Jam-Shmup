using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hasHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private healthUI UI;
    [SerializeField] GameObject PollenPickup;
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
        if (dead && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FinishedDeath"))
        {
            if (isPlayer)
            {
                transform.localPosition = startingLocalPos;
                GetComponent<Animator>().Play("Default");
                //todo decrement life
            }
            else
            {
                Instantiate(PollenPickup, transform.position, transform.rotation);

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
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

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

                    GetComponent<AudioSource>().Play();

                    if (GetComponent<Animator>() != null)
                    {
                        GetComponent<Animator>().Play("Death");
                    }


                    dead = true;

                    //play animation
                }
            }
        }
    }

}
