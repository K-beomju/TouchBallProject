using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    [SerializeField] private float dirSpeed;
    public float moveSpeed;
    public float rotateSpeed;
    public bool isSlow = false;
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
                if (isStart)
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
        if (!isStart)
        {
            transform.Translate(Vector3.right * (dirSpeed * Time.deltaTime * moveSpeed), Space.World);
            transform.Rotate(new Vector3(0,0, 30 * rotateSpeed * Time.deltaTime));
        }

        if (cameraRs.OutScreenBall(transform.position))
        {
            gameObject.SetActive(false);
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            press.GameOverDirect();
            UiManager.Instance.GameOverUI();
            DataManager.Instance.UpdateBestScore();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Star"))
        {
            other.GetComponent<Star>().GoStarPanel();
        }    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Press"))
        {

            rotateSpeed *= -1f;
            dirSpeed *= -1f;
            if (!isSlow)
            {
                moveSpeed += 0.01f;
                rotateSpeed += 1;
            }
            if (!press.isChange)
                press.ChangePressTransform();

            if (!press.isGoldPress)
                DataManager.Instance.CurrentAddScore();
            else
            {
                DataManager.Instance.CurrentAddScore(3);
                UiManager.Instance.currentScore.ChangeGoldTextColor();
            }

            if (DataManager.Instance.CurrentScore % 5 == 0)
            {
                Handheld.Vibrate();
            }
            if (DataManager.Instance.CurrentScore % 4 == 0 && ItemManager.Instance.isSpawn)
            {
                ItemManager.Instance.SpawnItem();
            }
        }
    }


}
