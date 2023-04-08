using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    bool isCollected;
    void Start()
    {
        isCollected = false;
    }
    public bool GetIsCollected()
    {
        return isCollected;
    }
    public void SetCollected()
    {
        isCollected = true;
        transform.tag = "EmptyCup";
    }
}
