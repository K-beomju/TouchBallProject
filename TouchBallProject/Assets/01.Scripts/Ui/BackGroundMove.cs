using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundMove : MonoBehaviour
{
    private RawImage rawImage;
    private Rect uvRect;

    public float speed = 1.0f;
    public bool backStart = false;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        if (backStart)
        {
            uvRect = rawImage.uvRect;
            uvRect.y += Time.deltaTime * speed;
            rawImage.uvRect = uvRect;
        }
    }
}
