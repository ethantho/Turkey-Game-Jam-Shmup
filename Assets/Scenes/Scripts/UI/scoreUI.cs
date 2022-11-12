using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreUI : MonoBehaviour
{
    Text scoreText;
    [SerializeField] hasScore player;

    Subscription<ScoreEvent> scored;

    void Start()
    {
        scored = EventBus.Subscribe<ScoreEvent>(HasScored);
        scoreText = gameObject.GetComponent<Text>();
    }

    void HasScored(ScoreEvent slay)
    {
        scoreText.text = "Score" + player.ToString();
    }


    private void OnDestroy()
    {
        EventBus.Unsubscribe(scored);
    }
}
