using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem{
    private static string BuildingsSavePath = Application.dataPath + "/buildings.sav";
    private static string ResourcesSavePath = Application.dataPath + "/resources.sav";

    public static void SaveBuildings(List<Building> buildings){
        BuildingDataWrapper buildingData = new BuildingDataWrapper();
        buildingData.buildings = buildings;
        string data = JsonUtility.ToJson(buildingData);
        File.WriteAllText(BuildingsSavePath, data);
    }

    public static void SaveResources(int goldAmount, int gemAmount){
        ResourcesDataWrapper resourceData = new ResourcesDataWrapper();
        resourceData.goldAmount = goldAmount;
        resourceData.gemAmount = gemAmount;
        string data = JsonUtility.ToJson(resourceData);
        File.WriteAllText(ResourcesSavePath, data);
    }

    public static List<Building> LoadBuildings(){
        Debug.Log("Loading Buildings...");
        if(BuildingsSaveExists()){
            string data = File.ReadAllText(BuildingsSavePath);
            return JsonUtility.FromJson<BuildingDataWrapper>(data).buildings;
        }
        return null;
    }

    public static ResourcesDataWrapper LoadResources(){
        Debug.Log("Loading Resources...");
        if(ResourcesSaveExists()){
            string data = File.ReadAllText(ResourcesSavePath);
            return JsonUtility.FromJson<ResourcesDataWrapper>(data);
        }
        return null;
    }

    public static bool BuildingsSaveExists(){
        return File.Exists(BuildingsSavePath);
    }

    public static bool ResourcesSaveExists(){
        return File.Exists(ResourcesSavePath);
    }

    private class SaveObject{
        public int data;

    }
}
