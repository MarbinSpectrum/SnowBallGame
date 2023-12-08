using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPlatform : MonoBehaviour
{
    private bool check = false;
    private bool run = false;

    [SerializeField] private Animation      platformAni;
    [SerializeField] private Rigidbody2D    rigidbody2D;
    [SerializeField] private BoxCollider2D  boxCollider2D;
    [SerializeField] private BoxCollider2D  boxTrigger2D;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (check == false && collision.transform.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            check = true;
            platformAni.Play();
            Invoke("RunPlatForm", 2f);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (run)
        {
            boxCollider2D.enabled = false;
            boxTrigger2D.enabled = false;
            if(collision.transform.gameObject.layer == LayerMask.NameToLayer("Ball"))
            {
                ControlMng.ball.StopControll();
            }
        }
    }


    private void RunPlatForm()
    {
        run = true;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

}
