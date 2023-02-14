using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    [SerializeField] private float dirSpeed;
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpSpeed;
    public bool isSlow = false;
    [SerializeField] private GameObject dieEffect;

    [SerializeField] private CameraResolution cameraRs;
    [SerializeField] private Press press;
    [SerializeField] private BackGroundMove backGroundMove;

    private Rigidbody2D rb;
    private bool isStart = true;
    [SerializeField] private Ease ease;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        transform.DOMoveY(0.5f, 2).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject())
            {
                if (isStart)
                {
                    DOTween.KillAll();
                    isStart = false;
                    press.gameObject.SetActive(true);
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    UiManager.Instance.GameStartUI();
                }
                rb.velocity = new Vector2(0, jumpSpeed);
                SoundManager.Instance.PlayFXSound("Jump");
            }
        }

        if(isStart)
        transform.Rotate(new Vector3(0,0,-30 * Time.deltaTime));
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
            SoundManager.Instance.PlayFXSound("Dead");

        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Star"))
        {
            SoundManager.Instance.PlayFXSound("Star");
            other.GetComponent<Star>().GoStarPanel();
        }    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Press") && !press.isChange)
        {
            SoundManager.Instance.PlayFXSound("Press");
            DataManager.Instance.AddStar();
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
            {
                DataManager.Instance.CurrentAddScore();
                UiManager.Instance.currentScore.NoramlTextColor();
            }
            else
            {
                DataManager.Instance.CurrentAddScore(3);
                UiManager.Instance.currentScore.ChangeGoldTextColor();
            }

            if (DataManager.Instance.CurrentScore % 5 == 0)
            {
                if(SecurityPlayerPrefs.GetBool("Vibrate", true))
                Handheld.Vibrate();

                backGroundMove.UpBackSpeed();
            }
            if (DataManager.Instance.CurrentScore % 4 == 0 && ItemManager.Instance.isSpawn)
            {
                ItemManager.Instance.SpawnItem();
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
 
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


}
