using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : ParentSetButton
{
    private void Start() 
    {
        if(!SecurityPlayerPrefs.HasKey("Sound"))
        {
            SecurityPlayerPrefs.SetBool("Sound", true);
        }
        else
        {
            if(SecurityPlayerPrefs.GetBool("Sound", true))
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
        base.SetOnOff(SecurityPlayerPrefs.GetBool("Sound", true));
    }

    protected override void SetOn()
    {
        base.SetOn();
        SecurityPlayerPrefs.SetBool("Sound", true);   
    }

    protected override void SetOff()
    {
        base.SetOff();
        SecurityPlayerPrefs.SetBool("Sound", false);
    }
}
