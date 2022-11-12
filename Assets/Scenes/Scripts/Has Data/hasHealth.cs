using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hasHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private healthUI UI;

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UI.SetHealthUI(maxHealth);
    }



    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UI.UpdateHealthUI(currentHealth);

        if (currentHealth <= 0)
        {
            //death
        }
    }
}
