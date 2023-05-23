using UnityEngine;

public class ResourceModel : MonoBehaviour
{
    public int gemAmount = 0;
    public int goldAmount = 0;

    private void Awake(){
        LoadResources();
    }

    public bool DecreaseGold(int amount){
        if(HasEnoughGold(amount)) {
            this.goldAmount -= amount;
            SaveResources();
            return true;
        }
        return false;
    }

    public bool DecreaseGem(int amount){
        if(HasEnoughGem(amount)){
            this.gemAmount -= amount;
            SaveResources();
            return true;
        }
        return false;
    }

    public int GetGold(){
        return this.goldAmount;
    }

    public int GetGem(){
        return this.gemAmount;
    }

    public void AddGold(int amount){
        this.goldAmount += amount;
        SaveResources();
    }

    public void AddGem(int amount){
        this.gemAmount += amount;
        SaveResources();
    }

    public void ResetResources(){
        this.goldAmount = 10;
        this.gemAmount = 5;
        SaveResources();
    }

    private bool HasEnoughGold(int goldAmount){
        return this.goldAmount >= goldAmount;
    }

    private bool HasEnoughGem(int gemAmount){
        return this.gemAmount >= gemAmount;
    }

    private void SaveResources(){
        SaveSystem.SaveResources(goldAmount, gemAmount);
    }

    private void LoadResources(){
        if(SaveSystem.ResourcesSaveExists()){
            ResourcesDataWrapper dataWrapper = SaveSystem.LoadResources();
            this.goldAmount = dataWrapper.goldAmount;
            this.gemAmount = dataWrapper.gemAmount;
        }
    }

    

}