using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{

    private static MonsterManager instance = null;

    public static MonsterManager Instance
    {
        get
        {
            if (null == instance)
                instance = new GameObject("MonsterManager").AddComponent<MonsterManager>();

            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
