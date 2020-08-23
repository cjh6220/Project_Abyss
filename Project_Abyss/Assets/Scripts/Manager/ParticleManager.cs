using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{   
    private static ParticleManager instance = null;
    ParticleTable m_ParticleTableData;
    //private List<GameObject> particleList = new List<GameObject>();
    public GameObject usingParticleObj;
    public GameObject unusedParticleObj;

    public static ParticleManager Instance
    {
        get
        {
            if (null == instance)
                instance = new GameObject("ParticleManager").AddComponent<ParticleManager>();

            return instance;
        }
    }
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadParticleTable();
        //Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadParticleTable()
    {
        if (m_ParticleTableData != null)
            return;

        m_ParticleTableData = Resources.Load("Table/ParticleTable") as ParticleTable;
    }

    public void Init()
    {
        GameObject usingParticle = new GameObject("UsingParticle");
        GameObject unusedParticle = new GameObject("UnusedParticle");
        usingParticle.transform.SetParent(this.gameObject.transform);
        usingParticleObj = usingParticle;// Instantiate( usingParticle, this.gameObject.transform);
        unusedParticle.transform.SetParent(this.gameObject.transform);
        unusedParticleObj = unusedParticle;//Instantiate(unusedParticle, this.gameObject.transform);        
    }

    private GameObject LoadParticle(string particleName)
    {
        Debug.Log("파티클 차일드 몇개?" + unusedParticleObj.transform.childCount);
        for (int i = 0; i < unusedParticleObj.transform.childCount; i++) //리스트에서 파티클 오브젝트를 찾는다.
        {
            if (unusedParticleObj.transform.GetChild(i).name == particleName)
            {
                return unusedParticleObj.transform.GetChild(i).gameObject;
            }
        }

        for (int n = 0; n < m_ParticleTableData.dataArray.Length; n++) //리스트에 없다면 테이블에서 로드한다.
        {
            if (m_ParticleTableData.dataArray[n].Name == particleName)
            {
                GameObject particle = Resources.Load(m_ParticleTableData.dataArray[n].Path) as GameObject;
                GameObject obj = Instantiate(particle, unusedParticleObj.transform);
                obj.name = m_ParticleTableData.dataArray[n].Name;
                obj.GetComponent<ParticleChild>().time = m_ParticleTableData.dataArray[n].Time;
                obj.SetActive(false);
                return obj;
            }
        }
        return null;
    }    

    public void PlayParticle(string particleName, Vector3 worldPos)
    {
        GameObject obj = LoadParticle(particleName);        
        obj.SetActive(true);        
        obj.transform.SetParent(usingParticleObj.transform);        
        obj.transform.position = worldPos;        
        obj.GetComponent<ParticleChild>().PlayParticle();
    }
}
