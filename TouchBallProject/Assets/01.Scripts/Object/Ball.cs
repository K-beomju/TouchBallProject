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

    public Rigidbody2D rb { get; set; }
    private bool isStart = true;        // 처음 게임 시작 판별
    public bool isMove = false;         // 움직임 제어
    public bool isRotate = true;        // 처음 로테이션 제어 
    public bool isRetry = false;        // 리트라이를 했는지 안했는지 판별
    public bool isDone = false;         // 리트라이 했는지 판별하여 게임 종료
    public bool isDragShot = false;

    [SerializeField] private Ease ease;

    [SerializeField] private Vector2 minPower;
    [SerializeField] private Vector2 maxPower;
    [SerializeField] private float power;

    [SerializeField] private DragLine dragLine;
    private Vector3 startPoint;
    private Vector3 currentPoint;
    private Vector3 endPoint;
    private Camera mainCam;
    private Vector2 force;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }

    private void Start()
    {
        transform.DOMoveY(0.5f, 2).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (IsPointerOverUIObject()) return;

        if (!isDragShot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 15;
            }

            if (Input.GetMouseButton(0))
            {
                currentPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = 15;

                if (IsDrag() && !isDragShot)
                    dragLine.RenderLine(startPoint, currentPoint);
            }
        }





        if (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isStart)
            {
                isStart = false;
                isRotate = false;
                isMove = true;

                DOTween.KillAll();
                press.gameObject.SetActive(true);
                UiManager.Instance.GameStartUI();

                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            if (isRetry)
            {
                isMove = true;
                isDone = true;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                isRetry = false;
            }
            SoundManager.Instance.PlayFXSound("Jump");


            rb.velocity = new Vector2(0, jumpSpeed);

            endPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;


            if (IsDrag() && !isDragShot)
            {
                UiManager.Instance.dragLineGroup.ReloadDrag();
                force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                                    Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
                rb.AddForce(force * power, ForceMode2D.Impulse);
                dragLine.EndPoint();
            }


        }

        if (isRotate)
            transform.Rotate(new Vector3(0, 0, -30 * Time.deltaTime));
    }

    public bool IsDrag() => Vector2.Distance(startPoint, currentPoint) > 0.5f;

    private void FixedUpdate()
    {
        if (isMove)
        {
            transform.Translate(Vector3.right * (dirSpeed * Time.deltaTime * moveSpeed), Space.World);
            transform.Rotate(new Vector3(0, 0, 30 * rotateSpeed * Time.deltaTime));
        }

        if (cameraRs.OutScreenBall(transform.position))
        {
            gameObject.SetActive(false);
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            SoundManager.Instance.PlayFXSound("Dead");
            ItemManager.Instance.starList.ForEach(x => x.GameOverStar());
            isMove = false;

            if (isDone)
            {
                press.GameOverDirect();
                UiManager.Instance.GameOverUI();
                DataManager.Instance.UpdateBestScore();
            }
            else
            {
                UiManager.Instance.InterstitialRetryPopup.SetActive(true);

            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Star"))
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
                if (SecurityPlayerPrefs.GetBool("Vibrate", true))
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
