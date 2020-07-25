using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1_1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TableManager.Instance.Init();
        BackGroundManager.Instance.Init();
        GroundController.Instance.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
