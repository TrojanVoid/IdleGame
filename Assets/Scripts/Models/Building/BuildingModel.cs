using UnityEngine;

using System;
using System.Collections.Generic;


public class BuildingModel : MonoBehaviour
{
    public int buildingShape;
    public int gemCost;
    public int goldCost;
    public float rechargeDuration;
    public int gemYield;
    public int goldYield;

    private Vector3Int buildingShapeMatrix;

    

    public void CreateBuildingAtPosition(Vector3Int pos){
        Building building = new Building(pos, buildingShape, rechargeDuration, gemYield, goldYield);
        building.AddBuilding();    
    }

    public List<Building> GetBuildings(){
        return Building.GetBuildings();
    }


    /* BUILDING SHAPES
            1:  
            XX
            
            2:
            X
            XX

            3:
            XX
            XX

            4:
            XXX
            XX
    */
    // Matrices' values are determined by choosing the up-left-most tile as origin
    // and calculating the X and Y offsets of the additional tiles relative to the origin
    public List<Vector3Int> GetBuildingShapeOffsetMatrix(){
        switch(buildingShape){
            case 1: return new List<Vector3Int>{new Vector3Int(1,0)};
            case 2: return new List<Vector3Int>{new Vector3Int(0,1),new Vector3Int(1,1)};
            case 3: return new List<Vector3Int>{new Vector3Int(1,0),new Vector3Int(0,1),new Vector3Int(1,1)};
            case 4: return new List<Vector3Int>{new Vector3Int(1,0),new Vector3Int(0,1),new Vector3Int(1,1),new Vector3Int(2,0)};
            default: throw new ArgumentException(
                "Building index is out of bounds. Index must be in the interval of [1-4]");
        } 
    }
    public List<Vector3Int> GetBuildingShapeOffsetMatrixOfShape(int buildingShapeIndex){
        switch(buildingShapeIndex){
            case 1: return new List<Vector3Int>{new Vector3Int(1,0)};
            case 2: return new List<Vector3Int>{new Vector3Int(0,1),new Vector3Int(1,1)};
            case 3: return new List<Vector3Int>{new Vector3Int(1,0),new Vector3Int(0,1),new Vector3Int(1,1)};
            case 4: return new List<Vector3Int>{new Vector3Int(1,0),new Vector3Int(0,1),new Vector3Int(1,1),new Vector3Int(2,0)};
            default: throw new ArgumentException(
                "Building index is out of bounds. Index must be in the interval of [1-4]");
        }
        
    }
}