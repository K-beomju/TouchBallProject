using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed;
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
                }
                rb.velocity = new Vector2(0, 5);
            }
        }
    }

    private void FixedUpdate()
    {
        if(!isStart)
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
        cameraRs.OutScreenBall(transform.position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Press"))
        {
            speed *= -1;
            press.ChangePressTransform();
        }
    }

}
