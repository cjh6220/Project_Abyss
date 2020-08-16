using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssGameManager : MonoBehaviour
{
    private static AbyssGameManager instance = null;
    public GameObject player;
    public static AbyssGameManager Instance
    {
        get
        {
            if (null == instance)
                instance = new GameObject("AbyssGameManager").AddComponent<AbyssGameManager>();

            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        player = GameObject.Find("Miner");
    }

    public void Init()
    {

    }

    public GameObject GetPlayer
    {
        get { return player; }
    }
}
