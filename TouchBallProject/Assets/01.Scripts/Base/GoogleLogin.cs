using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class GoogleLogin : MonoBehaviour
{
    private void Start() 
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Login();    
    }

    private void Login()
    {
        if(PlayGamesPlatform.Instance.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success ) => {
                
            });
        }
    }
}
