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

    //애니메이션 상태 확인용
    bool isFarming = false;
    bool isJumping = false;
    bool isJumpDownEnd = true;

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
    [SerializeField] private SpriteRenderer weaponParticle;


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
        weaponParticle.enabled = false;
        InitRes();
    }

    void InitRes()
    {
        for(int i = 0; i < 5; i++)
        {
            if (i == 0) // idle 상태 이닛
            {
                topRes.Add(Resources.Load<Sprite>("Graphic/Equipment/Idle/Top/Miner_top_idle_normal"));
                bottomRes.Add(Resources.Load<Sprite>("Graphic/Equipment/Idle/Bottom/Miner_bottom_idle_normal"));
                glovesRes_L.Add(Resources.Load<Sprite>("Graphic/Equipment/Idle/Gloves/Miner_gloves_idle_normal_L"));
                glovesRes_R.Add(Resources.Load<Sprite>("Graphic/Equipment/Idle/Gloves/Miner_gloves_idle_normal_R"));
                shoesRes_L.Add(Resources.Load<Sprite>("Graphic/Equipment/Idle/Shoes/Miner_shoes_idle_normal_L"));
                shoesRes_R.Add(Resources.Load<Sprite>("Graphic/Equipment/Idle/Shoes/Miner_shoes_idle_normal_R"));
            }
            else //walk 상태 이닛
            {
                topRes.Add(Resources.Load<Sprite>("Graphic/Equipment/Walk/Top/Miner_top_walk_normal_" + i));
                bottomRes.Add(Resources.Load<Sprite>("Graphic/Equipment/Walk/Bottom/Miner_bottom_walk_normal_" + i));
                if (i == 4) // walk 상태일때 4번 왼쪽 장갑 없음
                {
                    glovesRes_L.Add(Resources.Load<Sprite>("Graphic/Empty_alpha_img"));
                }     
                else
                {
                    glovesRes_L.Add(Resources.Load<Sprite>("Graphic/Equipment/Walk/Gloves/Miner_gloves_walk_normal_L_" + i));
                }
                glovesRes_R.Add(Resources.Load<Sprite>("Graphic/Equipment/Walk/Gloves/Miner_gloves_walk_normal_R_" + i));
                shoesRes_L.Add(Resources.Load<Sprite>("Graphic/Equipment/Walk/Shoes/Miner_shoes_walk_normal_L_" + i));
                shoesRes_R.Add(Resources.Load<Sprite>("Graphic/Equipment/Walk/Shoes/Miner_shoes_walk_normal_R_" + i));
            }
        }
        for(int n = 1; n < 5; n++) //jump 상태 이닛
        {
            topRes.Add(Resources.Load<Sprite>("Graphic/Equipment/Jump/Top/Miner_top_jump_normal_" + n));
            bottomRes.Add(Resources.Load<Sprite>("Graphic/Equipment/Jump/Bottom/Miner_bottom_jump_normal_" + n));
            if (n == 1) //jump 상태일때 1번 왼쪽 장갑 없음
            {
                glovesRes_L.Add(Resources.Load<Sprite>("Graphic/Empty_alpha_img"));
            }
            else
            {
                glovesRes_L.Add(Resources.Load<Sprite>("Graphic/Equipment/Jump/Gloves/Miner_gloves_jump_normal_L_" + n));
            }
            glovesRes_R.Add(Resources.Load<Sprite>("Graphic/Equipment/Jump/Gloves/Miner_gloves_jump_normal_R_" + n));
            shoesRes_L.Add(Resources.Load<Sprite>("Graphic/Equipment/Jump/Shoes/Miner_shoes_jump_normal_L_" + n));
            shoesRes_R.Add(Resources.Load<Sprite>("Graphic/Equipment/Jump/Shoes/Miner_shoes_jump_normal_R_" + n));
        }
        for(int a = 1; a < 4; a++)
        {
            topRes.Add(Resources.Load<Sprite>("Graphic/Equipment/Farming/Top/Miner_top_farming_normal_" + a));
            bottomRes.Add(Resources.Load<Sprite>("Graphic/Equipment/Farming/Bottom/Miner_bottom_farming_normal_" + a));
            glovesRes_L.Add(Resources.Load<Sprite>("Graphic/Equipment/Farming/Gloves/Miner_gloves_farming_normal_L_" + a));
            glovesRes_R.Add(Resources.Load<Sprite>("Graphic/Equipment/Farming/Gloves/Miner_gloves_farming_normal_R_" + a));
            shoesRes_L.Add(Resources.Load<Sprite>("Graphic/Equipment/Farming/Shoes/Miner_shoes_farming_normal_L_" + a));
            shoesRes_R.Add(Resources.Load<Sprite>("Graphic/Equipment/Farming/Shoes/Miner_shoes_farming_normal_R_" + a));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Farming();
        if (!isJumpDownEnd)
        {
            JumpRayCheck();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region //Move 관련 코드
    void Move()
    {
        if (!isFarming)
        {
            Vector3 moveVelocity = Vector3.zero;
            if (Input.GetKey(KeyCode.RightArrow) || variableJoystick.Horizontal > 0)
            {
                RaycastMoveCheck(Vector3.right);
                if (isContackRight == false)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    moveVelocity = Vector3.right;
                }
                ani.SetBool("IsMove", true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || variableJoystick.Horizontal < 0)
            {
                RaycastMoveCheck(Vector3.left);
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
    }
    void RaycastMoveCheck(Vector3 vector3) // 벽이나 넘지 못하는 땅 감지를 위해 레이캐스트
    {
        Vector2 rayVec;
        if (vector3 == Vector3.right) // 오른쪽 이동일때
        {
            rayVec = Vector2.right;
            //Debug.DrawRay(rayCastObj.transform.position, Vector3.right * BoxCollider2D.size.x * 0.6f, Color.red);
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
            //Debug.DrawRay(rayCastObj.transform.position, Vector3.left * BoxCollider2D.size.x * 0.6f, Color.red);
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
    #endregion

    #region //Jump 관련 코드
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("점프키 누름");
            if (jumpCount == 0 && isJumping == false)
            {
                rig.velocity = Vector2.zero;
                Vector2 jumpVelocity = new Vector2(0, jumpPower);
                rig.AddForce(jumpVelocity, ForceMode2D.Impulse);
                ani.SetBool("IsJumpUp", true);
                ani.SetBool("IsJumpDown", false);
                ani.SetBool("IsDoubleJumpUp", false);
                ani.SetBool("IsDoubleJumpDown", false);
                jumpCount += 1;
                Debug.Log("1단 점프키 누름");
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
                Debug.Log("2단 점프키 누름");
            }
        }        
    }

    public void ClickJump()
    {
        Debug.Log("점프키 누름");
        if (jumpCount == 0 && isJumping == false)
        {
            rig.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rig.AddForce(jumpVelocity, ForceMode2D.Impulse);
            ani.SetBool("IsJumpUp", true);
            ani.SetBool("IsJumpDown", false);
            ani.SetBool("IsDoubleJumpUp", false);
            ani.SetBool("IsDoubleJumpDown", false);
            jumpCount += 1;
            Debug.Log("1단 점프키 누름");
        }
        else if (jumpCount == 1)
        {
            rig.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rig.AddForce(jumpVelocity, ForceMode2D.Impulse);
            ani.SetBool("IsJumpUp", false);
            ani.SetBool("IsJumpDown", false);
            ani.SetBool("IsDoubleJumpUp", true);
            ani.SetBool("IsDoubleJumpDown", false);
            jumpCount += 1;
            Debug.Log("2단 점프키 누름");
        }
    }
    
    void JumpRayCheck() //현재 점프를 할 수 있는 상태인지 체크(바닥에 닿아 있을때 True)
    {   
        Vector3 rightBottomPos = new Vector3(rig.position.x + (BoxCollider2D.size.x * 0.5f), rig.position.y - (BoxCollider2D.size.y * 0.48f));
        Vector3 leftBottomPos = new Vector3(rig.position.x - (BoxCollider2D.size.x * 0.5f), rig.position.y - (BoxCollider2D.size.y * 0.48f));
        
        Debug.DrawRay(rightBottomPos, Vector3.down * 0.05f , Color.red);
        Debug.DrawRay(leftBottomPos, Vector3.down * 0.05f, Color.red);
        
        RaycastHit2D rayRight = Physics2D.Raycast(rightBottomPos, Vector3.down, 0.05f, LayerMask.GetMask("Ground"));
        RaycastHit2D rayLeft = Physics2D.Raycast(leftBottomPos, Vector3.down, 0.05f, LayerMask.GetMask("Ground"));
        
        if(rayRight.collider != null || rayLeft.collider != null)
        {
            Debug.Log("점프 체크 결과 true");
            isJumping = false;
            isJumpDownEnd = true;
            jumpCount = 0;
            ani.SetBool("IsJumpDown", false);
            ani.SetBool("IsDoubleJumpDown", false);
            ani.SetBool("IsJumpDownEnd", true);            
            //return true;
        }
        else
        {
            Debug.Log("점프 체크 결과 false");
            ani.SetBool("IsJumpDownEnd", false);
            isJumping = true;
            //return false;
        }
    }

    public void CheckJumpDownEnd()
    {
        isJumpDownEnd = false;
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
            Debug.Log("1단 점프 내려감 끝");
        }
        else if(jumpCount == 2)
        {
            ani.SetBool("IsJumpUp", false);
            ani.SetBool("IsJumpDown", false);
            ani.SetBool("IsDoubleJumpUp", false);
            ani.SetBool("IsDoubleJumpDown", true);
            jumpCount = 0;
            Debug.Log("2단 점프 내려감 끝");
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
            Debug.Log("1단 점프 올라감 끝");
        }
        else if(jumpCount == 2)
        {
            ani.SetBool("IsJumpUp", false);
            ani.SetBool("IsJumpDown", false);
            ani.SetBool("IsDoubleJumpUp", false);
            ani.SetBool("IsDoubleJumpDown", true);
            Debug.Log("2단 점프 올라감 끝");
        }
    }
    #endregion

    #region //애니메이션 장비 관련 코드
    public void MoveEquipmentChange(int i)//move = 1~4 사용
    {
        top.sprite = topRes[i];
        bottom.sprite = bottomRes[i];
        if (i != 4) //move일때 왼쪽 4번째 장갑 없음
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

    public void JumpEquipmentChange(int i)//jump = 5~8 사용
    {
        int n = i + 4;
        top.sprite = topRes[n];
        bottom.sprite = bottomRes[n];
        if (i != 1) //jump일때 왼쪽 1번째 장갑 없음
        {
            gloves_L.sprite = glovesRes_L[n];
        }
        else
        {
            gloves_L.sprite = null;
        }
        gloves_R.sprite = glovesRes_R[n];
        shoes_L.sprite = shoesRes_L[n];
        shoes_R.sprite = shoesRes_R[n];
    }

    public void FarmingEquipmentChange(int i)//farming = 9~11 사용
    {
        int n = i + 8;
        top.sprite = topRes[n];
        bottom.sprite = bottomRes[n];
        gloves_L.sprite = glovesRes_L[n];
        gloves_R.sprite = glovesRes_R[n];
        shoes_L.sprite = shoesRes_L[n];
        shoes_R.sprite = shoesRes_R[n];
    }

    public void IdleEquipmentChange()
    {
        top.sprite = Resources.Load<Sprite>("Graphic/Equipment/Idle/Top/Miner_top_idle_normal");
        bottom.sprite = Resources.Load<Sprite>("Graphic/Equipment/Idle/Bottom/Miner_bottom_idle_normal");
        gloves_L.sprite = Resources.Load<Sprite>("Graphic/Equipment/Idle/Gloves/Miner_gloves_idle_normal_L");
        gloves_R.sprite = Resources.Load<Sprite>("Graphic/Equipment/Idle/Gloves/Miner_gloves_idle_normal_R");
        shoes_L.sprite = Resources.Load<Sprite>("Graphic/Equipment/Idle/Shoes/Miner_shoes_idle_normal_L");
        shoes_R.sprite = Resources.Load<Sprite>("Graphic/Equipment/Idle/Shoes/Miner_shoes_idle_normal_R");
    }
    #endregion

    #region //파밍 관련 코드
    private void Farming()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(isJumping == false)
            {
                isFarming = true;
                weaponParticle.enabled = true;
                ani.SetTrigger("IsFarming");
                Debug.Log("키보드로 파밍");
            }
        }      
    }

    public void ClickUIFarming()
    {
        if (isJumping == false)
        {
            isFarming = true;
            weaponParticle.enabled = true;
            ani.SetTrigger("IsFarming");
            Debug.Log("클릭으로 파밍");
        }
    }

    public void CheckFarmingAni()
    {
        ani.SetBool("IsFarming", false);
        weaponParticle.enabled = false;
        isFarming = false;
        Debug.Log("애니메이션 끝남");
    }

    void OffEffect()
    {

    }
    #endregion    
}
