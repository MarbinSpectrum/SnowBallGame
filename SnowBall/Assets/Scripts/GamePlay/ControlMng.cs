using UnityEngine;

public class ControlMng : MonoBehaviour, MngInter
{
    public static ControlMng Instance;
    public void LoadMng() => Instance = this;

    private Ball ball;
    public Trajectory trajectory;

    [SerializeField]
    private float pushForce = 4f;
    [SerializeField]
    private float stopVelocity = 2f;
    [SerializeField]
    private float distanceMax = 4f;
    [SerializeField, Range(0, 1f)]
    private float decelerationValue = 0.1f;

    private bool isDragging = false;

    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 direction;
    private Vector2 force;
    private float distance;

    public bool  canControll { get; private set; }
    private bool downSoundFlag = false;

    private void Update()
    {
        if (ball == null || ball.gameObject.activeSelf == false)
        {
            //캐릭터 객체가 없어서 조종불가
            return;
        }

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
        GameObject ball = GameObject.Find("Ball");
        if (ball != null)
        {
            Instance.ball = ball.GetComponent<Ball>();
            Instance.ball.DesactivateRb();
        }

        Instance.canControll = true;
        Instance.downSoundFlag = false;
    }

    //-Drag-----------------------------------------------
    private void OnDragStart()
    {
        ball.DesactivateRb();
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        trajectory.Show();
    }

    private void OnDrag()
    {
        endPoint    = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
