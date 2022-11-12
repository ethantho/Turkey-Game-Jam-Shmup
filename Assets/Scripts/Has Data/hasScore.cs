using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  ScoreEvent
{
    public int currentScore;

    public ScoreEvent(int AddToScore)
    {
        this.currentScore = AddToScore;
    }
}

public class hasScore : MonoBehaviour
{
    //should only be attached to player
    [SerializeField] int score;

    private void Start()
    {
        score = 0;
    }

    public void Scored(int AddToScore)
    {
        score += AddToScore;
        EventBus.Publish<ScoreEvent>(new ScoreEvent(AddToScore));
    }

    public int GetScore()
    {
        return score;
    }
}
