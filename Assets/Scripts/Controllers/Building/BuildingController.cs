using UnityEngine;

using System.Collections.Generic;

public class BuildingController : MonoBehaviour{
    public BuildingModel model;

    public int GetGemCost(){
        return model.gemCost;
    }

    public int GetGoldCost(){
        return model.goldCost;
    }

    public List<Vector3Int> GetBuildingShapeOffsetMatrix(){
        return model.GetBuildingShapeOffsetMatrix();
    }

    public List<Vector3Int> GetBuildingShapeOffsetMatrixOfShape(int buildingShape){
        return model.GetBuildingShapeOffsetMatrixOfShape(buildingShape);
    }

    public void CreateBuildingAtPosition(Vector3Int pos){
        model.CreateBuildingAtPosition(pos);
    }

    public List<Building> GetBuildings(){
        return model.GetBuildings();
    }

}