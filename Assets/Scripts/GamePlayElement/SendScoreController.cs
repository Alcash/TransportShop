using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendScoreController : MonoBehaviour
{
    private IScoreSendable scoreSendable;

    private GamePlayState playState;

    private void Awake()
    {
        playState = FindObjectOfType<GamePlayState>();
        scoreSendable = GetComponent<IScoreSendable>();
    }

    private void OnEnable()
    {       
        if (scoreSendable != null)
        {
            scoreSendable.OnChangeScore += ChangeScoreHandler;
        }
    }

    private void OnDestroy()
    {
        if (scoreSendable != null)
        {
            scoreSendable.OnChangeScore -= ChangeScoreHandler;
        }
    }

    private void ChangeScoreHandler(int value)
    {
        if(playState != null)
             playState.ScoreCount += value;
    }
}
