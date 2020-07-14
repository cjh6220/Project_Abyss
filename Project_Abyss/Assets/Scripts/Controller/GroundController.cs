using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    GameObject tileMap;
    GameObject player;
    float moveSpeed = 5f;

    public GameObject GetGround { get { return tileMap; } }

    private static GroundController instance = null;

    public static GroundController Instance
    {
        get
        {
            if (null == instance)
                instance = new GameObject("GroundController").AddComponent<GroundController>();

            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //InitGround();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {

    }

    public void InitGround()
    {
        tileMap = GameObject.Find("Grid").gameObject.transform.GetChild(0).gameObject;
        player = GameObject.Find("Miner");
    }

    public void MoveGround(Vector3 moveDir)
    {
        Vector3 moveVelocity = -moveDir;
        if (player.transform.position.x >= 0)
        {
            tileMap.transform.position += moveSpeed * moveVelocity * Time.deltaTime;
        }
    }
}
