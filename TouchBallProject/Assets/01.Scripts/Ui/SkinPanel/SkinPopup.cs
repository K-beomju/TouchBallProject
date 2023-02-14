using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SkinPopup : MonoBehaviour
{
    public Action buyAction = () => {};
    public Text noStarText;

    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private void Awake() 
    {
        yesButton.onClick.AddListener(() => buyAction());
        noButton.onClick.AddListener(() => CancelSkin());
    }

    public void CancelSkin()
    {
        buyAction = null;
        gameObject.SetActive(false);
        noStarText.gameObject.SetActive(false);

    }


}
