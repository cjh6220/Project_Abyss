using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral_Iron : MineralBase
{
    MineralBase mMineral = null;
    private void Awake()
    {
        InitMineral(gameObject.name);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InitMineral(string name)
    {
        base.InitMineral(name);
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pick"))
        {
            DamagedMineral(10);
            if (GetHardness > 0)
            {
                ShakeMineral();
                ParticleManager.Instance.PlayParticle("Iron_piece", this.transform.position);
            }
        }        
    }

    public override void ShakeMineral()
    {
        base.ShakeMineral();
    }

    public override void DamagedMineral(int dmg)
    {
        base.DamagedMineral(dmg);
    }
}
