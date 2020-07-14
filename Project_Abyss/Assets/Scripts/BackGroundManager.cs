using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public GameObject backGroundObject;
    [SerializeField] protected GameObject[] backGround = new GameObject[3];
    [SerializeField] protected GameObject[] middleGround = new GameObject[3];
    public float spriteSize_X = 12.8f;
    public float backGround_MoveSpeed = 1f;
    public float middleGround_MoveSpeed = 3f;
    public float backStartPos;
    public float middleStartPos;
    int mainBackGround;
    GameObject player;

    private static BackGroundManager instance = null;

    
    public static BackGroundManager Instance
    {
        get
        {
            if (null == instance)
                instance = new GameObject("BackGroundManager").AddComponent<BackGroundManager>();

            return instance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InitBackGround();
        player = GameObject.Find("Miner");
    }

    // Update is called once per frame
    void Update()
    {
        //SwitchBackGround();
    }

    public void Init()
    {

    }
    void InitBackGround()
    {
        backGroundObject = GameObject.Find("BackGround");
        for(int i = 0; i < backGround.Length; i++)
        {
            backGround[i] = backGroundObject.transform.Find("Back").GetChild(i).gameObject;
            middleGround[i] = backGroundObject.transform.Find("Middle").GetChild(i).gameObject;
        }
        backStartPos = backGround[1].gameObject.transform.localPosition.x;
        middleStartPos = middleGround[1].gameObject.transform.localPosition.x;
        mainBackGround = 1;
    }

    public void SwitchBackGround(Vector3 moveDir)
    {
        Vector3 moveVelocity = -moveDir;

        if (player.transform.position.x >= 0 && player.transform.position.x <= 20f)
        {
            for (int i = 0; i < backGround.Length; i++)
            {
                backGround[i].transform.localPosition += moveVelocity * backGround_MoveSpeed * Time.deltaTime;
                float iPosX = backGround[i].transform.localPosition.x;
                if (iPosX <= backStartPos - spriteSize_X)
                {
                    backGround[i].transform.localPosition = new Vector3(iPosX + (spriteSize_X * 3f), 0f, 0f);
                }
            }
            for (int n = 0; n < middleGround.Length; n++)
            {
                middleGround[n].transform.localPosition += moveVelocity * middleGround_MoveSpeed * Time.deltaTime;
                float nPosX = middleGround[n].transform.localPosition.x;
                if (nPosX <= middleStartPos - spriteSize_X)
                {
                    middleGround[n].transform.localPosition = new Vector3(nPosX + (spriteSize_X * 3f), 0f, 0f);
                }
            }
        }

    }
}