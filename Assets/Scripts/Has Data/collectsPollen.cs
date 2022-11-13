using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectsPollen : MonoBehaviour
{
    [SerializeField] public healthUI pollenUI;
    [SerializeField] int maxPollen;

    int currentPollen;

    private void Start()
    {
        pollenUI.SetUI(maxPollen);
        pollenUI.UpdateUI(0);
    }

    public void CollectedPollen(int numPoints)
    {
        currentPollen = Mathf.Min(currentPollen + numPoints,maxPollen);
        pollenUI.UpdateUI(currentPollen);
    }

    public int CurrentPollen()
    {
        return currentPollen;
    }

    public void UsePollen()
    {
        currentPollen = 0;
        pollenUI.UpdateUI(currentPollen);
    }
}
