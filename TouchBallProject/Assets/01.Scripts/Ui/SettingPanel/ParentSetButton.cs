using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ParentSetButton : MonoBehaviour
{
    [SerializeField] private Sprite[] setSprites;

    private Button button;
    private Image image;

    private void Awake() 
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(() =>  SetOnOff(default));
    }

    protected virtual void SetOnOff(bool _status)
    {
        if(_status)
        {
            SetOff();
        }
        else
        {
            SetOn();
        }

    }

    protected virtual void SetOn()
    {
        image.sprite = setSprites[1];

    }

    protected virtual void SetOff()
    {
        image.sprite = setSprites[0];

    }
}
