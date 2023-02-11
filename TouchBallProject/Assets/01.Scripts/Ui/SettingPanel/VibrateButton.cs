using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrateButton : ParentSetButton
{
    private void Start() 
    {
        if(!SecurityPlayerPrefs.HasKey("Vibrate"))
        {
            SecurityPlayerPrefs.SetBool("Vibrate", true);
        }
        else
        {
            if(SecurityPlayerPrefs.GetBool("Vibrate", true))
            {
                SetOn();
            }
            else
            {
                SetOff();
            }
        }
    }

    protected override void SetOnOff(bool _status)  
    {
        base.SetOnOff(SecurityPlayerPrefs.GetBool("Vibrate", true));
    }

    protected override void SetOn()
    {
        base.SetOn();
        SecurityPlayerPrefs.SetBool("Vibrate", true);   
    }

    protected override void SetOff()
    {
        base.SetOff();
        SecurityPlayerPrefs.SetBool("Vibrate", false);
    }
}
