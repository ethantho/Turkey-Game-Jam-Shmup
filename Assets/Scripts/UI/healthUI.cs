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

    public void SetUI(float maxHealthIn)
    {
        maxHealth = maxHealthIn;

        maxWidth = GetComponent<RectTransform>().sizeDelta.x;
        height = GetComponent<RectTransform>().sizeDelta.y;

        centralPosition = transform.localPosition;
    }

    public void UpdateUI(float currentHealthIn)
    {
        //shrink and shift green part of UI dependent on what % of max health they have
        currentHealth = Mathf.Clamp(currentHealthIn, 0, maxHealth);


        float newWidth = maxWidth * (currentHealth / maxHealth);

        GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, height);

        float toShift = (maxWidth - newWidth) / 2.0f;
        GetComponent<RectTransform>().localPosition = centralPosition - new Vector3(toShift, 0, 0);
    }
}
