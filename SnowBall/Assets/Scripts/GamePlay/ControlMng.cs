using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMng : MonoBehaviour
{
    #region Singleton class: ControlMng

    public static ControlMng Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    private Camera                  cam;

    public  Ball                    ball;
    public  Trajectory              trajectory;

    [SerializeField] 
    private float                   pushForce = 4f;
    [SerializeField]
    private float                   stopVelocity = 2f;
    [SerializeField]
    private float                   distanceMax = 4f;
    [SerializeField,Range(0,1f)]
    private float decelerationValue = 0.1f;

    private bool                    isDragging = false;

    private Vector2                 startPoint;
    private Vector2                 endPoint;
    private Vector2                 direction;
    private Vector2                 force;
    private float                   distance;

    public  bool                    canControll { get;  private set;  }
    private bool                    downSoundFlag = false;
    //----------------------------------------------------
    private void Start()
    {
        lnit();
    }

    private void Update()
    {
        if (canControll)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                OnDragStart();
            }
            if (isDragging && Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                OnDragEnd();
            }

            if (isDragging)
            {
                OnDrag();
            }
        }
        else if(ball.isGround)
        {
            if(ball.rb.velocity.y <= 0)
            {
                float len = Vector2.Distance(Vector2.zero, ball.rb.velocity);
                len = Mathf.Pow(len, decelerationValue);
                Vector2 velocityVec2 = ball.rb.velocity.normalized*len;
                ball.rb.velocity = velocityVec2;
                
                if(downSoundFlag == false)
                {
                    downSoundFlag = true;
                    ball.DownSound();
                }
            }

            float velocity = Vector2.Distance(Vector2.zero, ball.rb.velocity);
            if (velocity < stopVelocity)
            {
                ball.DesactivateRb();
                canControll = true;
            }
        }
        else
        {
            downSoundFlag = false;
        }
    }

    public static void lnit()
    {
        Instance.canControll = true;
        Instance.downSoundFlag = false;
        Instance.cam = Camera.main;
        Instance.ball.DesactivateRb();
    }

    //-Drag-----------------------------------------------
    private void OnDragStart()
    {
        ball.DesactivateRb();
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
    }

    private void OnDrag()
    {
        endPoint    = cam.ScreenToWorldPoint(Input.mousePosition);
        distance    = Vector2.Distance(startPoint, endPoint);
        distance = Mathf.Min(distanceMax, distance);

        direction   = (startPoint - endPoint).normalized;
        force       = direction * distance * pushForce;

        trajectory.UpdateDots(ball.objPos, force);
    }

    private void OnDragEnd()
    {
        ball.ActivateRb();
        ball.Push(force);

        trajectory.Hide();

        canControll = false;
    }
}
