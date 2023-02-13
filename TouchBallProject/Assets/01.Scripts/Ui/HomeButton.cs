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
        button.onClick.AddListener(() => LoadHome());
    }

    public void LoadHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
