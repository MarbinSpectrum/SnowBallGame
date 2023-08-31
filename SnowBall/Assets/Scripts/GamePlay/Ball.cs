using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector] public    Rigidbody2D        rb;
    [HideInInspector] public    CircleCollider2D   col;

    [SerializeField] private    Ball_Data           ballData;
    [SerializeField] private    SoundObj           jumpSound;
    [SerializeField] private    SoundObj           downSound;

    private const float COLLIDER_SIZE = 0.45f;

    public RaycastHit2D isGround { get; private set; }
    public RaycastHit2D isShadow { get; private set; }
    public RaycastHit2D isSnow   { get; private set; }

    private void Awake()
    {
        rb  = GetComponent<Rigidbody2D>();
        rb.sharedMaterial.bounciness = ballData.bounciness;
        rb.sharedMaterial.friction   = ballData.friction;

        col = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        isGround = Physics2D.CircleCast(
            new Vector2(transform.position.x, transform.position.y - 0.1f) +
            new Vector2(col.offset.x, col.offset.y) * transform.localScale.x,
            col.radius * transform.localScale.x, Vector2.zero, 0, 1 << LayerMask.NameToLayer("Ground"));

        isShadow = Physics2D.CircleCast(
            new Vector2(transform.position.x, transform.position.y) +
            new Vector2(col.offset.x, col.offset.y) * transform.localScale.x,
            col.radius * transform.localScale.x, Vector2.zero, 0, 1 << LayerMask.NameToLayer("Shadow"));

        isSnow = Physics2D.CircleCast(
            new Vector2(transform.position.x, transform.position.y) +
            new Vector2(col.offset.x, col.offset.y) * transform.localScale.x,
            col.radius * transform.localScale.x, Vector2.zero, 0, 1 << LayerMask.NameToLayer("SnowZone"));

        float size = transform.localScale.x;

        if(isSnow)
        {
            size += Time.timeScale * ballData.erectionValue;
            size = Mathf.Min(1, size);
            transform.localScale = Vector3.one * size;
            if(isGround)
            {
                transform.position += Vector3.up* Time.timeScale * ballData.erectionValue;
            }
        }
        else if (isShadow == false)
        {
            size -= Time.timeScale * ballData.meltValue;
            transform.localScale = Vector3.one * size;
        }

        col.radius = COLLIDER_SIZE * size;

        if (size < 0.01f)
        {
            GameMng.GameOver();
            return;
        }
    }

    public Vector3 objPos { get => transform.position; }

    public void Push(Vector2 force)
    {
        //물체에 force만큼의 힘을 준다.
        rb.AddForce(force, ForceMode2D.Impulse);

        float distance = Vector2.Distance(Vector2.zero, force) 
            * transform.localScale.x * ballData.rotateValue;

        if (force.x > 0)
            rb.AddTorque(-distance);
        else
            rb.AddTorque(distance);

        jumpSound.Play();
    }

    public void ActivateRb()
    {
        //물체 물리 활성화
        rb.isKinematic = false;
    }

    public void DesactivateRb()
    {
        //물체 물리 비활성화
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }

    public void DownSound()
    {
        downSound.Play();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.timeScale == 0)
            return;

        if(collision.transform.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
        {
            GameMng.GameOver();
        }
        else if (collision.transform.gameObject.layer == LayerMask.NameToLayer("ClearPoint"))
        {
            GameMng.GoNext();
        }
    }
}
