using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstargramButton : MonoBehaviour
{
    private Button button;

    private void Awake() 
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => InstargramChannel());
    }

    public void InstargramChannel()
    {
        Application.OpenURL("https://instagram.com/happyhops_official?igshid=YmMyMTA2M2Y=");
    }
}
