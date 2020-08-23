using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleChild : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayParticle()
    {   
        if(time != 0)
        {
            Invoke("StopParticle", time);
        }
    }

    public void StopParticle()
    {
        transform.SetParent(ParticleManager.Instance.unusedParticleObj.transform);
        gameObject.SetActive(false);
    }
}
