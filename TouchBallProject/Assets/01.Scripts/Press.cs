using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour
{
    public void ChangePressTransform()
    {
        transform.position = new Vector3(-transform.position.x, Random.Range(-4,5),0);
    }
}
