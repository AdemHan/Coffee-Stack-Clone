using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckBools : MonoBehaviour
{
    Collisions collisions;
    bool _cremaCup;
    bool _emptyCup;
    bool _filledCup;
    bool _withPipette;
    bool _lidCup;
    

    

    void Start()
    {
        collisions = GetComponent<Collisions>();

        _emptyCup = true;
        _cremaCup = false;
        _filledCup = false;
        _withPipette = false;
        _lidCup = false;
    }

    public void SetCremaCupSelected()
    {
        _cremaCup = true;
        _emptyCup = false;
        _filledCup = false;
        _withPipette = false;
        _lidCup = false;
        
        collisions.EmptyCup.SetActive(false);
        collisions.CremaCup.SetActive(true);
        collisions.FilledCup.SetActive(false);
        collisions.WithPipette.SetActive(false);
        collisions.LidCup.SetActive(false);

    }
    public void SetEmptyCupSelected()
    {
        _cremaCup = false;
        _emptyCup = true;
        _filledCup = false;
        _withPipette = false;
        _lidCup = false;
        
        collisions.EmptyCup.SetActive(true);
        collisions.CremaCup.SetActive(false);
        collisions.FilledCup.SetActive(false);
        collisions.WithPipette.SetActive(false);
        collisions.LidCup.SetActive(false);

    }
    public void SetFilledCupSelected()
    {
        _cremaCup = false;
        _emptyCup = false;
        _filledCup = true;
        _withPipette = false;
        _lidCup = false;

        collisions.EmptyCup.SetActive(false);
        collisions.CremaCup.SetActive(false);
        collisions.FilledCup.SetActive(true);
        collisions.WithPipette.SetActive(false);
        collisions.LidCup.SetActive(false);

    }
    public void SetWithPipetteSelected()
    {
        _cremaCup = false;
        _emptyCup = false;
        _filledCup = false;
        _withPipette = true;
        _lidCup = false;

        collisions.EmptyCup.SetActive(false);
        collisions.CremaCup.SetActive(false);
        collisions.FilledCup.SetActive(false);
        collisions.WithPipette.SetActive(true);
        collisions.LidCup.SetActive(false);

    }
    public void SetLidCupSelected()
    {
        _cremaCup = false;
        _emptyCup = false;
        _filledCup = false;
        _withPipette = false;
        _lidCup = true;

        collisions.EmptyCup.SetActive(false);
        collisions.CremaCup.SetActive(false);
        collisions.FilledCup.SetActive(false);
        collisions.WithPipette.SetActive(false);
        collisions.LidCup.SetActive(true);

    }

    public bool isEmptyCupSelected()
    {
        if(_emptyCup)
        {
            return true;
        }
        else{
            return false;
        }  
    }
    public bool isCremaCupSelected()
    {
        if(_cremaCup)
        {
            return true;
        }
        else{
            return false;
        }  
    }
    public bool isFilledCupSelected()
    {
        if(_filledCup)
        {
            return true;
        }
        else{
            return false;
        }  
    }
    public bool isWithPipetteSelected()
    {
        if(_withPipette)
        {
            return true;
        }
        else{
            return false;
        }  
    }
    public bool isLidCupSelected()
    {
        if(_lidCup)
        {
            return true;
        }
        else{
            return false;
        }  
    }

}
