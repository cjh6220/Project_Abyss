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
    public bool isContackBottom = false;
    [SerializeField] public GameObject rayCastObj;    
    RaycastHit2D hit2D;
    [SerializeField]public float rayLength = 0.2f;
    [SerializeField]public VariableJoystick variableJoystick;

    [SerializeField] private SpriteRenderer top;
    [SerializeField] private SpriteRenderer bottom;
    [SerializeField] private SpriteRenderer gloves_L;
    [SerializeField] private SpriteRenderer gloves_R;
    [SerializeField] private SpriteRenderer shoes_L;
    [SerializeField] private SpriteRenderer shoes_R;


    private List<Sprite> topRes = new List<Sprite>();
    private List<Sprite> bottomRes = new List<Sprite>();
    private List<Sprite> glovesRes_L = new List<Sprite>();
    private List<Sprite> glovesRes_R = new List<Sprite>();
    private List<Sprite> shoesRes_L = new List<Sprite>();
    private List<Sprite> shoesRes_R = new List<Sprite>();
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        ani = gameObject.GetComponent<Animator>();
        BoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        rayCastObj = gameObject.transform.GetChild(0).gameObject;
        InitRes();
    }

    void InitRes()
    {
        for(int i = 0; i < 5; i++)
        {
            if (i == 0)
            {

                topRes.Add(Resources.Load<Sprite>("Graphic/Equipment/Idle/Top/Miner_top_normal"));
                bottomRes.Add(Resources.Load<Sprite>("Graphic/Equipment/Idle/Bottom/Miner_bottom_normal"));
                glovesRes_L.Add(Resources.Load<Sprite>("Graphic/Equipment/Idle/Gloves/Miner_normal_gloves_L"));
                glovesRes_R.Add(Resources.Load<Sprite>("Graphic/Equipment/Idle/Gloves/Miner_normal_gloves_R"));
                shoesRes_L.Add(Resources.Load<Sprite>("Graphic/Equipment/Idle/Shoes/Miner_normal_shoes_L"));
                shoesRes_R.Add(Resources.Load<Sprite>("Graphic/Equipment/Idle/Shoes/Miner_normal_shoes_R"));

                //topRes[i] = Resources.Load<Sprite>("Graphic/Equipment/Idle/Top/Miner_top_normal");                
                //bottomRes[i] = Resources.Load<Sprite>("Graphic/Equipment/Idle/Bottom/Miner_bottom_normal");
                //glovesRes_L[i] = Resources.Load<Sprite>("Graphic/Equipment/Idle/Gloves/Miner_normal_gloves_L");
                //glovesRes_R[i] = Resources.Load<Sprite>("Graphic/Equipment/Idle/Gloves/Miner_normal_gloves_R");
                //shoesRes_L[i] = Resources.Load<Sprite>("Graphic/Equipment/Idle/Shoes/Miner_normal_shoes_L");
                //shoesRes_R[i] = Resources.Load<Sprite>("Graphic/Equipment/Idle/Shoes/Miner_normal_shoes_R");
            }
            else
            {
                topRes.Add(Resources.Load<Sprite>("Graphic/Equipment/Walk/Top/Miner_top_normal_walk_" + i));
                bottomRes.Add(Resources.Load<Sprite>("Graphic/Equipment/Walk/Bottom/Miner_bottom_normal_walk_" + i));
                if (i != 4)
                {
                    glovesRes_L.Add(Resources.Load<Sprite>("Graphic/Equipment/Walk/Gloves/Miner_normal_gloves_walk_L_" + i));
                }                
                glovesRes_R.Add(Resources.Load<Sprite>("Graphic/Equipment/Walk/Gloves/Miner_normal_gloves_walk_R_" + i));
                shoesRes_L.Add(Resources.Load<Sprite>("Graphic/Equipment/Walk/Shoes/Miner_normal_shoes_walk_L_" + i));
                shoesRes_R.Add(Resources.Load<Sprite>("Graphic/Equipment/Walk/Shoes/Miner_normal_shoes_walk_R_" + i));

                //topRes[i] = Resources.Load<Sprite>("Graphic/Equipment/Walk/Top/Miner_top_normal_walk_" + i);
                //bottomRes[i] = Resources.Load<Sprite>("Graphic/Equipment/Walk/Bottom/Miner_bottom_normal_walk_" + i);
                //if (i != 4)
                //{
                //    glovesRes_L[i] = Resources.Load<Sprite>("Graphic/Equipment/Walk/Gloves/Miner_normal_gloves_walk_L_" + i);
                //}
                //else
                //{
                //    glovesRes_L[i] = null;
                //}
                //glovesRes_R[i] = Resources.Load<Sprite>("Graphic/Equipment/Walk/Gloves/Miner_normal_gloves_walk_R_" + i);
                //shoesRes_L[i] = Resources.Load<Sprite>("Graphic/Equipment/Walk/Shoes/Miner_normal_shoes_walk_L_" + i);
                //shoesRes_R[i] = Resources.Load<Sprite>("Graphic/Equipment/Walk/Shoes/Miner_normal_shoes_walk_R_" + i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
              
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.RightArrow) || variableJoystick.Horizontal > 0)
        {
            RaycastCheck(Vector3.right);
            if (isContackRight == false)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                moveVelocity = Vector3.right;
            }
            ani.SetBool("IsMove", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || variableJoystick.Horizontal < 0)
        {
            RaycastCheck(Vector3.left);            
            if (isContackLeft == false)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                moveVelocity = Vector3.left;
            }
            ani.SetBool("IsMove", true);            
        }
        else
        {
            isContackRight = false;
            isContackLeft = false;
            ani.SetBool("IsMove", false);
        }
        BackGroundManager.Instance.SwitchBackGround(moveVelocity);
        //GroundController.Instance.MoveGround(moveVelocity);
        transform.position += moveVelocity * movePower * Time.deltaTime;        
    }
    void RaycastCheck(Vector3 vector3) // 벽이나 넘지 못하는 땅 감지를 위해 레이캐스트
    {
        Vector2 rayVec;
        if (vector3 == Vector3.right) // 오른쪽 이동일때
        {
            rayVec = Vector2.right;
            hit2D = Physics2D.Raycast(rayCastObj.transform.position, rayVec * 0.1f);            
            if (hit2D.collider != null) // 레이캐스트에 부딛힌 물체가 있을때
            {
                if (hit2D.collider.tag == "Ground") // 해당 태그가 그라운드일때
                {
                    float distance = Mathf.Abs(hit2D.point.x - rayCastObj.transform.position.x);
                    if (distance < rayLength) // 물체가 거리에 있을 경우
                    {   
                        isContackRight = true;
                        isContackLeft = false;
                    }
                    else // 태그가 그라운드 이지만 거리에는 안들어옴
                    {
                        isContackRight = false;
                        isContackLeft = false;
                    }
                }
            }
            else // 오른쪽 이동이 들어오긴 했지만 부딛힌 물체는 없을 때
            {
                isContackRight = false;
                isContackLeft = false;
            }
        }
        else if (vector3 == Vector3.left)
        {
            rayVec = Vector2.left;
            hit2D = Physics2D.Raycast(rayCastObj.transform.position, rayVec * 0.1f);            
            if (hit2D.collider != null)
            {
                if (hit2D.collider.tag == "Ground")
                {
                    float distance = Mathf.Abs(hit2D.point.x - rayCastObj.transform.position.x);
                    if (distance < rayLength)
                    {
                        isContackLeft = true;
                        isContackRight = false;
                    }
                    else
                    {
                        isContackLeft = false;
                        isContackRight = false;
                    }
                }
            }
            else
            {
                isContackRight = false;
                isContackLeft = false;
            }
        }
        else
        {
            isContackRight = false;
            isContackLeft = false;
        }
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

    public void MoveEquipmentChange(int i)
    {
        top.sprite = topRes[i];
        bottom.sprite = bottomRes[i];
        if (i != 4)
        {
            gloves_L.sprite = glovesRes_L[i];
        }
        else
        {
            gloves_L.sprite = null;
        }
        gloves_R.sprite = glovesRes_R[i];
        shoes_L.sprite = shoesRes_L[i];
        shoes_R.sprite = shoesRes_R[i];
    }

    public void IdleEquipmentChange()
    {
        top.sprite = Resources.Load<Sprite>("Graphic/Equipment/Idle/Top/Miner_top_normal");
        bottom.sprite = Resources.Load<Sprite>("Graphic/Equipment/Idle/Bottom/Miner_bottom_normal");
        gloves_L.sprite = Resources.Load<Sprite>("Graphic/Equipment/Idle/Gloves/Miner_normal_gloves_L");
        gloves_R.sprite = Resources.Load<Sprite>("Graphic/Equipment/Idle/Gloves/Miner_normal_gloves_R");
        shoes_L.sprite = Resources.Load<Sprite>("Graphic/Equipment/Idle/Shoes/Miner_normal_shoes_L");
        shoes_R.sprite = Resources.Load<Sprite>("Graphic/Equipment/Idle/Shoes/Miner_normal_shoes_R");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("콜리전 스테이 이름 = " + collision.transform.name);
        //Debug.Log("몇개 충돌중인가? = " + collision.contacts.Length);

        //if (collision.transform.name == "Ground")
        //{
        //    for (int i = 0; i < collision.contacts.Length; i++)
        //    {
        //        if (collision.contacts[i].normal.y >= 0.95)
        //        {
        //            //Debug.Log("바닥에 닿음");
        //            isContackBottom = true;
        //        }
        //        else if (collision.contacts[i].normal.y <= -0.95)
        //        {
        //            //Debug.Log("머리에 닿음");
        //        }

        //        if (collision.contacts[i].normal.x >= 0.95)
        //        {
        //            //Debug.Log("왼쪽 몸에 닿음");
        //            isContackLeft = true;
        //        }
        //        else if (collision.contacts[i].normal.x <= -0.95)
        //        {
        //            //Debug.Log("오른쪽 몸에 닿음");
        //            isContackRight = true;
        //        }
        //        else
        //        {
        //            isContackBottom = false;
        //            isContackLeft = false;
        //            isContackRight = false;
        //            //Debug.Log("모든 컨텍 false");
        //        }
        //    }
        //}
    }
}
