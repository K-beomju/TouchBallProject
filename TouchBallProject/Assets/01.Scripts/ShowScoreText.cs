using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScoreText : MonoBehaviour
{
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text currentScoreText;


    public void ShowHighScore(int _score)
    {
        highScoreText.text = _score.ToString();
    }
    
    public void ShowCurrentScore(int _score)
    {
        currentScoreText.text = _score.ToString();
    }
}
