using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    private Button button;

    private void Awake() 
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => ShowAd());

        if(!SecurityPlayerPrefs.HasKey("CountAd"))
        {
            SecurityPlayerPrefs.SetInt("CountAd", 0);
        }
    }
    
    public void ShowAd()
    {
        int count = SecurityPlayerPrefs.GetInt("CountAd", default);
        ++count;

        Debug.Log(count);

        if(count % 2 == 0)
        AdManager.Instance.ShowInterstitial(() => LoadHome());
        else
        LoadHome();

        SecurityPlayerPrefs.SetInt("CountAd", count);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
