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
        button.onClick.AddListener(() =>  SetOnOff());
    }

    protected virtual void SetOnOff()
    {
        if(image.sprite == setSprites[0])
        {
            image.sprite = setSprites[1];
            SetOff();
        }
        else
        {
            image.sprite = setSprites[0];
            SetOn();
        }

    }

    protected abstract void SetOn();
    protected abstract void SetOff();
}
