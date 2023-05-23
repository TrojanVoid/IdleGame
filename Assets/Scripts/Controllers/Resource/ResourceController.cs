using UnityEngine;

using UnityEngine.UIElements;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    
    public ResourceModel model;
    public UIDocument UIDocument;

    public GameObject floatingNumberPrefab;

    public bool MakePurchase(int goldCost, int gemCost){
        return model.DecreaseGold(goldCost) && model.DecreaseGem(gemCost);
    }

    public bool CanPurchase(int goldCost, int gemCost){
        return model.GetGold() >= goldCost && model.GetGem() >= gemCost;
    }

    public int GetGold(){
        return model.GetGold();
    }

    public int GetGem(){
        return model.GetGem();
    }

    public void AddGold(int amount){
        model.AddGold(amount);
    }

    public void AddGem(int amount){
        model.AddGem(amount);
    }

    public void ResetResources(){
        model.ResetResources();
    }

}