using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Slime : MonsterBase
{
    // Start is called before the first frame update
    void Start()
    {
        InitMonster("Slime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InitMonster(string name)
    {
        base.InitMonster(name);
    }
}
