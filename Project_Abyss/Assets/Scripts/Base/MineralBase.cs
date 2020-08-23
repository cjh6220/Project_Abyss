using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralBase : MonoBehaviour
{
    [SerializeField] protected string mMineralName;
    [SerializeField] protected string mMineralBase;
    [SerializeField] protected int mHardness;
    protected Animator ani;
    protected SpriteRenderer sprite;
    public GameObject spriteObj;
    public BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void InitMineral(string name)
    {
        MineralTableData data = MineralManager.Instance.GetMineralData(name);

        mMineralName = data.Name;
        mMineralBase = data.Basemineral;
        mHardness = data.Hardness;
        ani = gameObject.GetComponent<Animator>();

        spriteObj = gameObject.transform.GetChild(0).gameObject;
        sprite = spriteObj.GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    public virtual void ShakeMineral()
    {
        ani.SetTrigger("IsShake");
    }

    public virtual void DamagedMineral(int dmg)
    {
        mHardness -= dmg;
        if(mHardness <= 0)
        {
            BrokenMineral();
        }
    }

    private void BrokenMineral()
    {
        Debug.Log(gameObject.name + "부서짐");
        ani.SetBool("IsBroken", true);
        sprite.sprite = Resources.Load<Sprite>("Mineral/Iron/" + mMineralBase + "_crumb");
        //Debug.Log("이전포지션 : " + spriteObj.transform.position);
        //spriteObj.transform.position = new Vector3(spriteObj.transform.position.x, spriteObj.transform.position.y - (col.size.y), spriteObj.transform.position.z);
        this.transform.position = new Vector3(transform.position.x, transform.position.y - (col.size.y * 0.35f), transform.position.z);
        //Debug.Log("이후포지션 : " + spriteObj.transform.position);
    }

    public int GetHardness
    {
        get { return mHardness; }
    }
}
