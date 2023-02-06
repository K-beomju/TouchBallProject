using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Press : MonoBehaviour
{
    public void ChangePressTransform()
    {
        float targetX = -transform.position.x;
        transform.position = new Vector3(targetX + (transform.position.x > 0 ? -0.5f : 0.5f), Random.Range(-4, 5), 0);
        transform.DOMoveX(targetX, 0.1f);
    }
}
