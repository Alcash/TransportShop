using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    private GamePlayState playState;

    [SerializeField]
    private Text text;

    private void Awake()
    {
        playState = FindObjectOfType<GamePlayState>();
    }

    private void ChangeScoreHandler()
    {
        if(text)
         text.text = playState.ScoreCount.ToString();
    }

    private void OnEnable()
    {
        if(playState)
            playState.OnChangeScore += ChangeScoreHandler;
        ChangeScoreHandler();
    }

    private void OnDisable()
    {
        if (playState)
            playState.OnChangeScore -= ChangeScoreHandler;
    }
}
