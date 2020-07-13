using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] protected GameObject[] backGround = new GameObject[3];
    [SerializeField] protected GameObject[] middleGround = new GameObject[3];
    public float spriteSize_X;
    public float backGround_MoveSpeed;
    public float middleGround_MoveSpeed;
    public float backStartPos;
    public float middleStartPos;
    int mainBackGround;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        backStartPos = backGround[1].gameObject.transform.localPosition.x;
        middleStartPos = middleGround[1].gameObject.transform.localPosition.x;
        mainBackGround = 1;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchBackGround();
    }

    void SwitchBackGround()
    {
        Vector3 moveVelocity = Vector3.left;

        for(int i = 0; i < backGround.Length; i++)
        {
            backGround[i].transform.localPosition += moveVelocity * backGround_MoveSpeed * Time.deltaTime;
            float iPosX = backGround[i].transform.localPosition.x;
            if (iPosX <= backStartPos - spriteSize_X)
            {
                backGround[i].transform.localPosition = new Vector3(iPosX + (spriteSize_X * 3f), 0f, 0f);
            }
        }
        for(int n = 0; n < middleGround.Length; n++)
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