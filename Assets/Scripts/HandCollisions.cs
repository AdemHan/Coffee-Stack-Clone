using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisions : MonoBehaviour
{
    BoxCollider boxCollider;
    private void Start() 
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Collectable" && other.gameObject.GetComponent<Collectable>().GetIsCollected() == false)
        {    
            GameManager.Instance.firstCoffee = other.gameObject;
            GameManager.Instance.z_index += GameManager.Instance.spaceBetweenParts;
            other.gameObject.GetComponent<Collectable>().SetCollected(); 
            GameManager.Instance.cupList.Add(other.gameObject);
            other.gameObject.transform.SetParent(this.gameObject.transform.parent);
            other.gameObject.transform.localPosition = Vector3.zero;
            GameManager.Instance.StartCoroutine(GameManager.Instance.WaveEffect());
            GameManager.Instance.gatheredMoney += GameManager.Instance.emptyCupPrice;
            GameManager.Instance.SetMoneyTexts();
        }
        if(other.gameObject.tag == "FinishLine")
        {
            StartCoroutine(GameManager.Instance.FinishGame());
        }
    }
    void Update() 
    {
        if(GameManager.Instance.cupList.Count <= 1)
        {
            GameManager.Instance.z_index = 0;
            boxCollider.enabled = true;
        }
        else{
            boxCollider.enabled = false;
        }
    }
}
