using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject ground;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        InitCamera();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void InitCamera()
    {
        ground = GameObject.Find("Grid").transform.GetChild(0).gameObject;
        player = GameObject.Find("Miner");
    }

    void MoveCamera()
    {
        if (player.transform.position.x >= 0 && player.transform.position.x <= 20f)
        {
            gameObject.transform.position = new Vector3(player.transform.position.x, 0, -10f);
        }
    }
}

