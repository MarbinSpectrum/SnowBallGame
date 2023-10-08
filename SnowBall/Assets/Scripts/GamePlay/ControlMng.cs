using UnityEngine;

public class ControlMng : MonoBehaviour, MngInter
{
    public static ControlMng Instance;
    public static Ball ball { get; private set; }

    public void LoadMng() => Instance = this;

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
            }

            float velocity = Vector2.Distance(Vector2.zero, ball.rb.velocity);
            if (velocity < stopVelocity)
            {
                ball.DesactivateRb();
                canControll = true;
            }
        }
    }

    public static void lnit()
    {
        GameObject ballObj = GameObject.Find("Ball");
        if (ballObj != null)
        {
            ball = ballObj.GetComponent<Ball>();
            ball.DesactivateRb();
        }

        Instance.canControll = true;
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
        StopControll();
        ball.Push(force);
    }

    //-Controll-----------------------------------------------
    public void StopControll()
    {
        ball.ActivateRb();
        trajectory.Hide();
        canControll = false;
    }
}
