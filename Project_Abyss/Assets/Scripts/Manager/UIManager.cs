using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private List<GameObject> btnObj = new List<GameObject>();
    private List<GameObject> popupObj = new List<GameObject>();
    private GameObject canvas;

    private static UIManager instance = null;
    
    public static UIManager Instance
    {
        get
        {
            if (null == instance)
                instance = new GameObject("UIManager").AddComponent<UIManager>();

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void InitListener()
    {
        btnObj[0].GetComponent<Button>().onClick.AddListener(OpenInventory);
        btnObj[1].GetComponent<Button>().onClick.AddListener(ClickFarming);
        btnObj[2].GetComponent<Button>().onClick.AddListener(ClickJump);
    }

    void InitBtnObj()
    {
        GameObject btn = canvas.transform.Find("Btn").gameObject;
        for(int i=0; i < btn.transform.childCount; i++)
        {
            btnObj.Add(btn.transform.GetChild(i).gameObject);
        }
    }

    void PopupObj()
    {
        GameObject btn = canvas.transform.Find("Popup").gameObject;
        for (int i = 0; i < btn.transform.childCount; i++)
        {
            popupObj.Add(btn.transform.GetChild(i).gameObject);
            popupObj[i].SetActive(false);
        }
    }
    public void Init()
    {

    }
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        InitBtnObj();
        PopupObj();
        InitListener();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OpenInventory()
    {
        if(popupObj[0].activeSelf == false)
        {
            popupObj[0].gameObject.SetActive(true);
        }
        else
        {
            popupObj[0].gameObject.SetActive(false);
        }
    }

    private void ClickFarming()
    {
        AbyssGameManager.Instance.GetPlayer.GetComponent<CharactorController>().ClickUIFarming();
    }

    private void ClickJump()
    {
        AbyssGameManager.Instance.GetPlayer.GetComponent<CharactorController>().ClickJump();
    }
}
