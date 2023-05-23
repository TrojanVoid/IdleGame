using UnityEngine;

public class ResourceModel : MonoBehaviour
{
    public int gemAmount = 0;
    public int goldAmount = 0;

    public bool DecreaseGold(int amount){
        if(HasEnoughGold(amount)) {
            this.goldAmount -= amount;
            return true;
        }
        return false;
    }

    public bool DecreaseGem(int amount){
        if(HasEnoughGem(amount)){
            this.gemAmount -= amount;
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
    }

    public void AddGem(int amount){
        this.gemAmount += amount;
    }

    private bool HasEnoughGold(int goldAmount){
        return this.goldAmount >= goldAmount;
    }

    private bool HasEnoughGem(int gemAmount){
        return this.gemAmount >= gemAmount;
    }

    

}