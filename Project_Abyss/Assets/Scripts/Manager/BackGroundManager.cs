﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public GameObject backGroundObject;
    [SerializeField] protected GameObject[] backGround = new GameObject[3];
    [SerializeField] protected GameObject[] middleGround = new GameObject[3];
    public float spriteSize_X = 12.8f;
    public float backGround_MoveSpeed = -1f;
    public float middleGround_MoveSpeed = 0.1f;
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
        if (player == null)
            return;

        if (player.transform.position.x >= 0 && player.transform.position.x <= 20f)
        {
            //backGroundObject.transform.position += moveVelocity * middleGround_MoveSpeed * Time.deltaTime;
            for (int i = 0; i < backGround.Length; i++)
            {
                backGround[i].transform.position += moveVelocity * backGround_MoveSpeed * Time.deltaTime;
                float iPosX = backGround[i].transform.position.x;
                if (player.transform.position.x - iPosX > 14.8f && moveDir.x > 0)//iPosX <= player.transform.position.x - spriteSize_X)
                {
                    backGround[i].transform.position = new Vector3(iPosX + (spriteSize_X * 3f), backGround[i].transform.position.y, backGround[i].transform.position.z);
                }
                else if(player.transform.position.x - iPosX < -14.8f && moveDir.x < 0)
                {
                    backGround[i].transform.position = new Vector3(iPosX - (spriteSize_X * 3f), backGround[i].transform.position.y, backGround[i].transform.position.z);
                }
            }
            for (int n = 0; n < middleGround.Length; n++)
            {
                middleGround[n].transform.position += moveVelocity * middleGround_MoveSpeed * Time.deltaTime;
                float nPosX = middleGround[n].transform.position.x;
                if (player.transform.position.x - nPosX > 14.8f && moveDir.x > 0)
                {
                    middleGround[n].transform.position = new Vector3(nPosX + (spriteSize_X * 3f), middleGround[n].transform.position.y, middleGround[n].transform.position.z);
                }
                else if(player.transform.position.x - nPosX < -14.8f && moveDir.x < 0)
                {
                    middleGround[n].transform.position = new Vector3(nPosX - (spriteSize_X * 3f), middleGround[n].transform.position.y, middleGround[n].transform.position.z);
                }
            }
        }

    }
}