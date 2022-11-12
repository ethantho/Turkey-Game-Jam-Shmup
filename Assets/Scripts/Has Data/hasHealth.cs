using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hasHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private healthUI UI;

    private bool isPlayer;
    int currentHealth;
    Vector3 startingLocalPos;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        startingLocalPos = transform.localPosition;

        if (GetComponent<PlayerController>() != null)
        {
            isPlayer = true;
            UI.SetHealthUI(maxHealth);
        }
        else
        {
            isPlayer = false;
        }
    }



    public void ChangeHealth(int amount)
    {
        //update health
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        //update UI
        if (isPlayer)
        {
            UI.UpdateHealthUI(currentHealth);
        }

        //death
        if (currentHealth <= 0)
        {
            if (isPlayer)
            {
                transform.localPosition = startingLocalPos;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
