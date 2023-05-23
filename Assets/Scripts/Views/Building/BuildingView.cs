using UnityEngine;
using UnityEngine.Tilemaps;

using System.Collections.Generic;
using UnityEngine.UI;

public class BuildingView : MonoBehaviour{
    public ResourceController resourceController;
    public Tilemap tilemap;
    public GameObject progressBarPrefab;
    public GameObject floatingNumberPrefab;


    private Dictionary<Vector3Int, GameObject> progressBars;
    private List<Building> buildings;

    private void Start(){
        progressBars = new Dictionary<Vector3Int, GameObject>();
    }
    
    private void Update(){
        buildings = Building.GetBuildings();
        
        if(Building.GetBuildings() != null){
            foreach(Building building in Building.GetBuildings()){
                HandleBuildingProgressBar(building);
            }
        }
        else{
            ClearBuildingProgressBars();
        }
    }

    private void HandleBuildingProgressBar(Building building){
        Vector3Int buildingPosition = building.GetPosition();
        if(progressBars.ContainsKey(buildingPosition)){
            GameObject progressBar = progressBars[buildingPosition];
            float fillAmount = (float)building.GetRechargeCounter() / building.GetRechargeDuration();
            progressBar.GetComponent<Slider>().value = fillAmount;
            building.IncrementCounter(Time.deltaTime);

            if(building.GetRechargeCounter() >= building.GetRechargeDuration()){
                resourceController.AddGold(building.GetGoldYield());
                resourceController.AddGem(building.GetGemYield());

                GameObject floatingGoldReward = Instantiate(floatingNumberPrefab, tilemap.GetCellCenterWorld(buildingPosition), Quaternion.identity);
                Text goldRewardText = floatingGoldReward.GetComponentInChildren<Text>();
                goldRewardText.text = "+" + building.GetGoldYield().ToString();
                floatingGoldReward.transform.Find("Text").position = tilemap.GetCellCenterWorld(buildingPosition)
                + new Vector3(0f, 1f, 0f);
                goldRewardText.color = Color.yellow;
                
                GameObject floatingGemReward = Instantiate(floatingNumberPrefab, tilemap.GetCellCenterWorld(buildingPosition), Quaternion.identity);
                Text gemRewardText = floatingGemReward.GetComponentInChildren<Text>();
                gemRewardText.text = "+" + building.GetGemYield().ToString();
                floatingGemReward.transform.Find("Text").position = tilemap.GetCellCenterWorld(buildingPosition)
                + new Vector3(0.75f, 1f, 0f);
                gemRewardText.color = Color.blue;

                Destroy(floatingGoldReward, 2f); 
                Destroy(floatingGemReward, 2f);
                building.ResetRechargeCounter();
            }
        }
        else{
            CreateProgressBar(buildingPosition);
        }
    }

    private void ClearBuildingProgressBars(){
        progressBars = null;
    }

    private void CreateProgressBar(Vector3Int position){
        GameObject progressBar = Instantiate(progressBarPrefab, tilemap.GetCellCenterWorld(position), Quaternion.identity);
        progressBar.transform.Find("Bar").position = tilemap.GetCellCenterWorld(position);
        progressBars[position] = progressBar;
    }
}