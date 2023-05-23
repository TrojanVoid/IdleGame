using UnityEngine;

using System.Collections.Generic;

public class Building{
    private Vector3Int position;
    private int buildingShape;
    private float rechargeDuration;
    private int gemYield;
    private int goldYield;
    private float rechargeCounter;


    private static List<Building> Buildings;

    public Building(Vector3Int pos, int buildingShape, float rechargeDuration, int gemYield, int goldYield){
        if(Buildings == null) Buildings = new List<Building>();
        this.position = pos;
        this.buildingShape = buildingShape;
        this.rechargeDuration = rechargeDuration;
        this.gemYield = gemYield;
        this.goldYield = goldYield;
        this.rechargeCounter = 0;
    }

    public void AddBuilding(){
        Buildings.Add(this);
    }

    
    public void IncrementCounter(float deltaTime){
        this.rechargeCounter += deltaTime;
    }

    public void ResetRechargeCounter(){
        this.rechargeCounter = 0;
    }

    public Vector3Int GetPosition(){
        return this.position;
    }

    public float GetRechargeDuration(){
        return this.rechargeDuration;
    }

    public int GetGemYield(){
        return this.gemYield;
    }

    public int GetGoldYield(){
        return this.goldYield;
    }

    public int GetBuildingShape(){
        return this.buildingShape;
    }

    public float GetRechargeCounter(){
        return this.rechargeCounter;
    }

    public static List<Building> GetBuildings(){
        return Buildings;
    }
    
}