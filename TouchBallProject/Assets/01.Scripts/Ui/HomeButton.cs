using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    [SerializeField] private Button homeButton;
    private void Awake() 
    {

        if(!SecurityPlayerPrefs.HasKey("CountAd"))
        {
            SecurityPlayerPrefs.SetInt("CountAd", 0);
        }
    }
    
    public void ShowAd()
    {
        int count = SecurityPlayerPrefs.GetInt("CountAd", default);
        ++count;

        if (count % 5 == 0)
            AdManager.Instance.ShowInterstitialAd(() => LoadHome());
        else
            LoadHome();

        SecurityPlayerPrefs.SetInt("CountAd", count);
        homeButton.interactable = false;
    }

    public void LoadHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AdManager.Instance.RequestInterstitial();

    }
}
