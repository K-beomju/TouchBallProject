using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlayFXSound("Item");
            ItemManager.Instance.UseItem();
            gameObject.SetActive(false);
        }
    }
}
