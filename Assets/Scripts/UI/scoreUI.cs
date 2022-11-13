using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreUI : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    [SerializeField] hasScore player;

    Subscription<ScoreEvent> scored;

    void Start()
    {
        scored = EventBus.Subscribe<ScoreEvent>(HasScored);
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void HasScored(ScoreEvent slay)
    {
        scoreText.SetText("Score: " + player.GetScore().ToString());
    }


    private void OnDestroy()
    {
        EventBus.Unsubscribe(scored);
    }
}
