using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthUI : MonoBehaviour
{
    float maxWidth;
    float height;

    float currentHealth;
    float maxHealth;
    Vector3 centralPosition;

    public void SetHealthUI(float maxHealthIn)
    {
        maxHealth = maxHealthIn;

        maxWidth = GetComponent<RectTransform>().sizeDelta.x;
        height = GetComponent<RectTransform>().sizeDelta.y;

        centralPosition = transform.localPosition;
    }

    public void UpdateHealthUI(float currentHealthIn)
    {
        //shrink and shift green part of UI dependent on what % of max health they have
        currentHealth = currentHealthIn;

        float newWidth = maxWidth * (currentHealth / maxHealth);

        GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, height);

        float toShift = (maxWidth - newWidth) / 2.0f;
        GetComponent<RectTransform>().localPosition = centralPosition - new Vector3(toShift, 0, 0);
    }
}