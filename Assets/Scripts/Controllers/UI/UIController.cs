using UnityEngine;
using UnityEngine.UIElements;

using System.Collections.Generic;
using System.Linq;

public class UIController : MonoBehaviour
{

    public ResourceController resourceController;
    public GameObject buildings;
    private List<BuildingController> buildingControllers;

    private void OnEnable(){
        buildingControllers = GetBuildingControllers();
    }

    public bool BuildingCanBePurchased(int buildingIndex){
        BuildingController controller = buildingControllers[buildingIndex];
        return resourceController.CanPurchase(controller.GetGoldCost(), controller.GetGemCost());
    }


    private List<BuildingController> GetBuildingControllers(){
        return buildings.GetComponentsInChildren<BuildingController>().ToList();
    }

    
    public int GetGold(){
        return resourceController.GetGold();
    }

    public int GetBuildingGoldCost(int buildingIndex){
        return GetBuildingControllers()[buildingIndex].GetGoldCost();
    }

    public int GetBuildingGemCost(int buildingIndex){
        return GetBuildingControllers()[buildingIndex].GetGemCost();
    }

    public List<Vector3Int> GetBuildingShapeOffsetMatrix(int buildingIndex){
        return GetBuildingControllers()[buildingIndex].GetBuildingShapeOffsetMatrix();
    }

    public List<Vector3Int> GetBuildingShapeOffsetMatrixOfShape(int buildingShape){
        return GetBuildingControllers()[0].GetBuildingShapeOffsetMatrixOfShape(buildingShape);
    }

    public void CreateBuildingAtPosition(int buildingIndex, Vector3Int pos){
        GetBuildingControllers()[buildingIndex].CreateBuildingAtPosition(pos);
    }

    public List<Building> GetBuildings(){
        return GetBuildingControllers()[0].GetBuildings();
    }

    public int GetGem(){
        return resourceController.GetGem();
    }


}
