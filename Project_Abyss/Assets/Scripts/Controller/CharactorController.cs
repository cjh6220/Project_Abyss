using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorController : MonoBehaviour
{
    public float movePower = 5f;
    public float jumpPower = 5f;    
    private int jumpCount = 0;
    Rigidbody2D rig;
    Animator ani;
    BoxCollider2D BoxCollider2D;
    public bool isContackLeft = false;
    public bool isContackRight = false;
    


    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        ani = gameObject.GetComponent<Animator>();
        BoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();        
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if(isContackRight == false)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                moveVelocity = Vector3.right;
            }
            ani.SetBool("IsMove", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (isContackLeft == false)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                moveVelocity = Vector3.left;
            }
            ani.SetBool("IsMove", true);            
        }
        else
        {
            ani.SetBool("IsMove", false);
        }
        BackGroundManager.Instance.SwitchBackGround(moveVelocity);
        //GroundController.Instance.MoveGround(moveVelocity);
        transform.position += moveVelocity * movePower * Time.deltaTime;        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount == 0)
            {
                rig.velocity = Vector2.zero;
                Vector2 jumpVelocity = new Vector2(0, jumpPower);
                rig.AddForce(jumpVelocity, ForceMode2D.Impulse);
                ani.SetBool("IsJumpUp", true);
                ani.SetBool("IsJumpDown", false);
                ani.SetBool("IsDoubleJumpUp", false);
                ani.SetBool("IsDoubleJumpDown", false);
                jumpCount += 1;
            }
            else if(jumpCount == 1)
            {
                rig.velocity = Vector2.zero;
                Vector2 jumpVelocity = new Vector2(0, jumpPower);
                rig.AddForce(jumpVelocity, ForceMode2D.Impulse);
                ani.SetBool("IsJumpUp", false);
                ani.SetBool("IsJumpDown", false);
                ani.SetBool("IsDoubleJumpUp", true);
                ani.SetBool("IsDoubleJumpDown", false);
                jumpCount += 1;
            }
        }        
    }

    public void CheckJump_Down_End()
    {
        if (jumpCount == 1)
        {   
            ani.SetBool("IsJumpUp", false);
            ani.SetBool("IsJumpDown", true);
            ani.SetBool("IsDoubleJumpUp", false);
            ani.SetBool("IsDoubleJumpDown", false);
            jumpCount = 0;            
        }
        else if(jumpCount == 2)
        {
            ani.SetBool("IsJumpUp", false);
            ani.SetBool("IsJumpDown", false);
            ani.SetBool("IsDoubleJumpUp", false);
            ani.SetBool("IsDoubleJumpDown", true);
            jumpCount = 0;
        }
    }

    public void CheckJump_Up_End()
    {
        if (jumpCount == 1)
        {
            ani.SetBool("IsJumpUp", false);
            ani.SetBool("IsJumpDown", true);
            ani.SetBool("IsDoubleJumpUp", false);
            ani.SetBool("IsDoubleJumpDown", false);
        }
        else if(jumpCount == 2)
        {
            ani.SetBool("IsJumpUp", false);
            ani.SetBool("IsJumpDown", false);
            ani.SetBool("IsDoubleJumpUp", false);
            ani.SetBool("IsDoubleJumpDown", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("콜리전 이름 = " + collision.transform.name);
        //Debug.Log("몇개 충돌중인가? = " + collision.contacts.Length);
        //if (collision.transform.name == "Ground")
        //{
        //    if(collision.contacts[0].normal.y >= 0.95)
        //    {
        //        Debug.Log("바닥에 닿음");
        //    }
        //    else if(collision.contacts[0].normal.y <= -0.95)
        //    {
        //        Debug.Log("머리에 닿음");
        //    }

        //    if(collision.contacts[0].normal.x >= 0.95 || collision.contacts[0].normal.x <= -0.95)
        //    {
        //        Debug.Log("몸에 닿음");
        //    }
        //    //for(int i = 0; i < collision.contacts.Length; i++)
        //}
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("콜리전 스테이 이름 = " + collision.transform.name);
        //Debug.Log("몇개 충돌중인가? = " + collision.contacts.Length);
        if (collision.transform.name == "Ground")
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                if (collision.contacts[i].normal.y >= 0.95)
                {
                    //Debug.Log("바닥에 닿음");
                }
                else if (collision.contacts[i].normal.y <= -0.95)
                {
                    //Debug.Log("머리에 닿음");
                }

                if (collision.contacts[i].normal.x >= 0.95)
                {
                    //Debug.Log("왼쪽 몸에 닿음");
                    isContackLeft = true;
                }
                else if (collision.contacts[i].normal.x <= -0.95)
                {
                    //Debug.Log("오른쪽 몸에 닿음");
                    isContackRight = true;
                }
                else
                {
                    isContackLeft = false;
                    isContackRight = false;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("탈출한 콜리전 이름 = " + collision.transform.name);
        if(collision.transform.name == "Ground")
        {
            isContackLeft = false;
            isContackRight = false;
        }
    }
}
