using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collisions : MonoBehaviour
{
    public GameObject CremaCup,EmptyCup,FilledCup,WithPipette,LidCup;
    CheckBools checkBools;
    Transform collectedCups;
    public int totalCupPrice;
    
    

    void Awake()
    {
        CremaCup = this.gameObject.transform.GetChild(0).gameObject;
        EmptyCup = this.gameObject.transform.GetChild(1).gameObject;
        FilledCup = this.gameObject.transform.GetChild(2).gameObject;
        WithPipette = this.gameObject.transform.GetChild(3).gameObject;
        LidCup = this.gameObject.transform.GetChild(4).gameObject;
        checkBools = GetComponent<CheckBools>();
        collectedCups = GameObject.Find("CollectedCups").transform;
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Collectable" && other.gameObject.GetComponent<Collectable>().GetIsCollected() == false)
        {   
            gameObject.GetComponent<CheckBools>().SetEmptyCupSelected();
            GameManager.Instance.z_index += GameManager.Instance.spaceBetweenParts;
            other.gameObject.GetComponent<Collectable>().SetCollected(); 
            GameManager.Instance.cupList.Add(other.gameObject);
            GameManager.Instance.StartCoroutine(GameManager.Instance.WaveEffect());
            other.gameObject.transform.SetParent(collectedCups);
            GameManager.Instance.gatheredMoney += GameManager.Instance.emptyCupPrice;
            GameManager.Instance.SetMoneyTexts();
            
        }

        if(other.gameObject.tag == "FlowingCoffee" && checkBools.isEmptyCupSelected())
        {
            totalCupPrice += GameManager.Instance.filledCupPrice;
            GameManager.Instance.gatheredMoney += GameManager.Instance.filledCupPrice;
            GameManager.Instance.SetMoneyTexts();

            gameObject.GetComponent<CheckBools>().SetFilledCupSelected();
            StartCoroutine(GameManager.Instance.DoScaleFunction(gameObject));

            
        }
        
        if(other.gameObject.tag == "CremaCupGate" && 
        (checkBools.isFilledCupSelected() || checkBools.isEmptyCupSelected()))
        {
            checkBools.SetCremaCupSelected();
            totalCupPrice += GameManager.Instance.cremaCupPrice;
            GameManager.Instance.gatheredMoney += GameManager.Instance.cremaCupPrice;
            GameManager.Instance.SetMoneyTexts();
            // StartCoroutine(GameManager.Instance.DoScaleFunction(gameObject));
        }

        if(other.gameObject.tag == "SellField")
        {
            float cupPosX = gameObject.transform.position.x;
            float cupPosY = gameObject.transform.position.y;
            float cupPosZ = gameObject.transform.position.z;

            GameManager.Instance.moneyParticalEffect.transform.position = new Vector3(cupPosX, cupPosY + 2f, cupPosZ);
            GameManager.Instance.moneyParticalEffect.Play();
            StartCoroutine(GameManager.Instance.SellCups(this.gameObject , other.gameObject.GetComponent<BoxCollider>(), totalCupPrice));         
        }
        if(other.gameObject.tag == "LidMaker")
        {
            totalCupPrice += GameManager.Instance.lidCupPrice;
            GameManager.Instance.gatheredMoney += GameManager.Instance.lidCupPrice;
            GameManager.Instance.SetMoneyTexts();

            checkBools.SetLidCupSelected();
            StartCoroutine(GameManager.Instance.DoScaleFunction(gameObject));

            
        }

        if(other.gameObject.tag == "Barricades")
        {
            float cupPosX = gameObject.transform.position.x;
            float cupPosY = gameObject.transform.position.y;
            float cupPosZ = gameObject.transform.position.z;

            
            GameManager.Instance.totalMoney -= totalCupPrice;
            GameManager.Instance.gatheredMoney -= totalCupPrice;
            

            GameManager.Instance.cupList.Remove(this.gameObject);
            GameManager.Instance.z_index -= GameManager.Instance.spaceBetweenParts;

            GameManager.Instance.dustParticalEffect.transform.position = new Vector3(cupPosX, cupPosY + 2f, cupPosZ);
            GameManager.Instance.dustParticalEffect.Play();

            totalCupPrice = 0;
            GameManager.Instance.SetMoneyTexts();
            Destroy(this.gameObject);

            
        }

        if(other.gameObject.tag == "FinishLine")
        {
            StartCoroutine(GameManager.Instance.FinishGame());
        }
        if(other.gameObject.tag == "Destroyer")
        {
            float cupPosX = gameObject.transform.position.x;
            float cupPosY = gameObject.transform.position.y;
            float cupPosZ = gameObject.transform.position.z;
            
            GameManager.Instance.totalMoney += totalCupPrice;
            GameManager.Instance.gatheredMoney -= totalCupPrice;
            GameManager.Instance.SetMoneyTexts();

            GameManager.Instance.moneyParticalEffect.transform.position = new Vector3(cupPosX, cupPosY + 2f, cupPosZ);
            GameManager.Instance.moneyParticalEffect.Play();
            GameManager.Instance.cupList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

}
