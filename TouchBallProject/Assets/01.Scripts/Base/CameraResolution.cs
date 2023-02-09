using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private Camera mainCam;

    private void Awake()
    {
        Application.targetFrameRate = 300;
        mainCam = GetComponent<Camera>();

        //Rect rect = mainCam.rect;
        //float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16);
        //float scaleWidth = 1f / scaleheight;

        //if (scaleheight < 1)
        //{
        //    rect.height = scaleheight;
        //    rect.y = (1f - scaleheight) / 2f;
        //}
        //else
        //{
        //    rect.width = scaleWidth;
        //    rect.x = (1f - scaleWidth) / 2f;
        //}
        //mainCam.rect = rect;
    }

    public bool OutScreenBall(Vector3 lockPos)
    {
        Vector3 pos = mainCam.WorldToViewportPoint(lockPos);
        if (pos.x < 0f || pos.x > 1f || pos.y > 1f || pos.y < 0f) 
        {
            return true;
        }
        return false;
    }
}
