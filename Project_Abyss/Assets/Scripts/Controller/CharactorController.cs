using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorController : MonoBehaviour
{
    public float movePower = 5f;
    public float jumpPower = 5f;
    //private bool isJumping = false;
    Rigidbody2D rig;
    Animator ani;

    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        ani = gameObject.GetComponent<Animator>();
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
            ani.SetBool("IsMove", true);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            moveVelocity = Vector3.right;            
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            ani.SetBool("IsMove", true);
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            moveVelocity = Vector3.left;
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
            rig.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rig.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }        
    }
}
