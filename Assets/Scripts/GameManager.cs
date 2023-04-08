using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static GameManager _instance;
    public static GameManager Instance
    {
     get {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                
                if (_instance == null)
                {
                    GameObject container = new GameObject("Bicycle");
                    _instance = container.AddComponent<GameManager>();
                }
            }
     
            return _instance;
        }  
    }
    #endregion
    
    CheckBools checkBools;
    [HideInInspector] public float z_index;
    public GameObject player;
    public GameObject collectedCups;
    public GameObject Bills;
    public List<GameObject> cupList;
    [Space(25)]
    public GameObject rootCoffee;
    public GameObject firstCoffee;
    public GameObject inCanvasCup1,inCanvasCup2,inCanvasCup3;
    public ParticleSystem dustParticalEffect;
    public ParticleSystem moneyParticalEffect;
    [SerializeField] private float rotateSpeed;
    public float _waveSpeed = 0.1f;
    public float _waveTimeBetweenToCup = 0.125f;
    public float cupScaleIncrease = 1.1f;
    public float spaceBetweenParts = 1.15f;
    [Range(.25f,1f)] [SerializeField] private float smoothTime = .5f;
    [SerializeField] private float playerMaxHorizontalSpeed = 150f;
    [Space(30)]
    [HideInInspector] public int totalMoney;
    [HideInInspector] public int gatheredMoney;
    
    [Range(1,2)] public int emptyCupPrice = 1;
    [Range(1,3)] public int filledCupPrice = 2;
    [Range(1,3)] public int cremaCupPrice = 3;
    [Range(1,4)] public int lidCupPrice = 4;
    public GameObject virtualCam1;
    public GameObject virtualCam2;
    public GameObject virtualCam3;
    public TextMeshProUGUI gatheredMoneyText;
    public TextMeshProUGUI totalMoneyText;
    public GameObject finishUI;

    
    void Awake()
    {
        Application.targetFrameRate = 60;
        z_index = 0f;
        cupList.Add(rootCoffee);
        cupList.Add(firstCoffee);
        rootCoffee.transform.position = new Vector3(rootCoffee.transform.position.x,
                        transform.position.y,transform.position.z - spaceBetweenParts);
        
        playerMaxHorizontalSpeed = playerMaxHorizontalSpeed * smoothTime;
        
        gatheredMoney = 1;
        totalMoney = 0;

    }
    void Start() 
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetPosition();
        if(cupList.Count <= 0)
        {
            SetMoneyTexts();
        }

        if(totalMoney < 0)
        {
            totalMoney = 0;
        }


    }

 
    public IEnumerator WaveEffect()
    {
        if(cupList.Count > 0)
        {
            for(int i = 1; i < cupList.Count + 1; i++)
            {
                var lastCup = cupList[cupList.Count - i];
                lastCup.transform.DOScale(cupScaleIncrease, _waveSpeed);
                yield return new WaitForSeconds(_waveTimeBetweenToCup);
                lastCup.transform.DOScale(1f, _waveSpeed);

            }
        }
    }
    public IEnumerator DoScaleFunction(GameObject go)
    {
        go.transform.DOScale(cupScaleIncrease, _waveSpeed);
        yield return new WaitForSeconds(1f);
        go.transform.DOScale(1f, _waveSpeed);
    }


    public void SetPosition()
    {
        int cupCount = cupList.Count;
        float playerPosX = rootCoffee.transform.position.x;
        float playerPosZ = rootCoffee.transform.position.z;
        

        if(cupList.Count > 1)
        {
            for(int i = 1; i < cupCount; i++)
            {
                cupList[i].transform.position = new Vector3(Mathf.Lerp(cupList[i].transform.position.x, cupList[i-1].transform.position.x,Time.deltaTime * 20),
                1.5f,playerPosZ + (i * spaceBetweenParts));

            }
        }
        
    }

    public IEnumerator SellCups(GameObject go , BoxCollider col, int totalCupPrice)
    {
        checkBools = go.GetComponent<CheckBools>();
        cupList.Remove(go);
        z_index -= spaceBetweenParts;

        totalMoney += totalCupPrice;
        gatheredMoney -= totalCupPrice;
        SetMoneyTexts();

        go.transform.SetParent(col.gameObject.transform);
        go.tag = "Selled";
        go.transform.localScale = new Vector3(0.2f, 0.35f, 0.15f);
        go.transform.localPosition = new Vector3(-0.275f, 0.1f, 0.33f);
        go.transform.DOLocalMoveX(1.4f, 1.25f);
        yield return new WaitForSeconds(1.25f);
        Destroy(go);
        
    }

    public void SetMoneyTexts()
    {
        if(gatheredMoney < 0)
        {
            gatheredMoney = 0;
        }
        gatheredMoneyText.text = gatheredMoney.ToString() + " $";
        totalMoneyText.text = totalMoney.ToString();
    }


    public IEnumerator FinishGame()
    {
        player.GetComponent<CharacterMove>().rb.velocity = Vector3.zero;
        virtualCam1.SetActive(false);
        virtualCam2.SetActive(true);
        virtualCam3.SetActive(false);

        collectedCups.transform.SetParent(player.transform);
        player.GetComponent<CharacterMove>().enabled = false;

        player.transform.DOMoveX(0.5f, 1f);
        yield return new WaitForSeconds(1f);
        player.transform.DOMove(new Vector3(0.25f, 1.75f, 222f), 4f);
        
        // Camera.main.GetComponent<CameraFollow>().enabled = false;
        virtualCam2.SetActive(false);
        virtualCam3.SetActive(true);


        StartCoroutine(DoMoveY());
    }

    IEnumerator DoMoveY()
    {
        yield return new WaitForSeconds(4.5f);
        player.transform.GetChild(0).transform.DOLocalRotateQuaternion(Quaternion.Euler(0f,180f,-90f),1.5f);
        Bills.transform.SetParent(player.transform);
        Bills.transform.localPosition = new Vector3(0.25f, 0.5f, 0f);
        yield return new WaitForSeconds(1f);

        player.transform.DOMoveY(((totalMoney-(totalMoney%5))/5) + 2.5f,
        (totalMoney-(totalMoney%5))/20 + 1f);
        yield return new WaitForSeconds((totalMoney-(totalMoney%5))/10 + 1f);
        finishUI.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
