using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    [SerializeField] private float dirSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject dieEffect;

    [SerializeField] private CameraResolution cameraRs;
    [SerializeField] private Press press;

    private Rigidbody2D rb;
    private bool isStart = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                if(isStart)
                {
                    isStart = false;
                    press.gameObject.SetActive(true);
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    UiManager.Instance.GameStartUI();
                }
                rb.velocity = new Vector2(0, 5);
            }
        }
    }

    private void FixedUpdate()
    {
        if(!isStart)
        transform.Translate(Vector2.right * (dirSpeed * Time.deltaTime * moveSpeed));
        
        if(cameraRs.OutScreenBall(transform.position))
        {
            gameObject.SetActive(false);
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            press.GameOverDirect();
            UiManager.Instance.GameOverUI();
            DataManager.Instance.UpdateBestScore();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Press"))
        {
            dirSpeed *= -1f;
            press.ChangePressTransform();
            DataManager.Instance.CurrentAddScore();


            // 5의 배수가 될 때 점점 어려워지게 || 이벤트 아이템 
            if(DataManager.Instance.CurrentScore % 5 == 0)
            {
                // 0.1 프로씩 증가 
                moveSpeed += 0.01f;

            }
        }
    }

}
