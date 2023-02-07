using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoSingleton<UiManager>
{
    public ScoreTextGroup scoreTextGroup;

    public void GameOverUI()
    {
        scoreTextGroup.GameOverDirect();

    }

}
