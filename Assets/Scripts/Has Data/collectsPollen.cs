using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectsPollen : MonoBehaviour
{
    [SerializeField] healthUI pollenUI;
    [SerializeField] int maxPollen;

    int currentPollen;

    private void Start()
    {
        pollenUI.SetUI(maxPollen);
        pollenUI.UpdateUI(0);
    }

    public void CollectedPollen()
    {
        currentPollen++;
        pollenUI.UpdateUI(currentPollen);
    }

    public int CurrentPollen()
    {
        return currentPollen;
    }

    public void UsePollen()
    {
        currentPollen = currentPollen - 10;
    }
}
