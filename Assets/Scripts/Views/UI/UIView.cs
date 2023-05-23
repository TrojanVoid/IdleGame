using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

using System.Collections.Generic;


public class UIView : MonoBehaviour {

    
    public UIController controller;
    public Tilemap tilemap;
    public ResourceController resourceController;
    public Material redTileMaterial;
    public Material greenTileMaterial;
    public TileBase buildingTile;
    public TileBase emptyTile;
    

    private UIDocument UIDocument;
    private List<Button> buildingButtons;
    private Button restartButton;
    private Label goldLabel;
    private Label gemLabel;
    private int selectedBuildingIndex;
    

    private bool isDragging;
    private bool canBuild;
    private List<Vector3Int> shapeMatrix;
    private List<Vector3Int> occupiedTilesMatrix;
    

    private void OnEnable(){
        Building.LoadBuildings();
        occupiedTilesMatrix = new List<Vector3Int>();
        UIDocument = GetComponent<UIDocument>();
        FetchUIElements();
        for(int i = 0; i < buildingButtons.Count; i++){
            int buildingIndex = i;
            Button button = buildingButtons[i];
            button.Q<Label>("GoldText").text = controller.GetBuildingGoldCost(buildingIndex).ToString();
            button.Q<Label>("GemText").text = controller.GetBuildingGemCost(buildingIndex).ToString();
            button.RegisterCallback<MouseDownEvent>(OnMouseDown, TrickleDown.TrickleDown);
            UIDocument.rootVisualElement.RegisterCallback<MouseUpEvent>(OnMouseUp);
        }
        restartButton.clicked += () => {
            Building.ResetBuildings();
            resourceController.ResetResources();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        };

        SetBuildingTiles();
    }

    private void Update(){
        UpdateBuildingAvailabilities();
        UpdateResourcesUI();
        HandleDragging();
    }

    private void OnMouseDown(MouseDownEvent e){
        if(e.button == (int)MouseButton.LeftMouse){
            selectedBuildingIndex = FindBuildingIndexFromButton(FindButtonFromChild((VisualElement)(e.target)));
            isDragging = true;            
        }
        e.StopPropagation();
    }

    private void OnMouseUp(MouseUpEvent e){
        isDragging = false;
        ResetTileColors();
        if(canBuild){
            controller.CreateBuildingAtPosition(selectedBuildingIndex, shapeMatrix[0]);
            resourceController.MakePurchase(
                controller.GetBuildingGoldCost(selectedBuildingIndex),
                controller.GetBuildingGemCost(selectedBuildingIndex));
            SetBuildingTiles();
        }
    }

    private Button FindButtonFromChild(VisualElement element){
        while(element != null){
            if(element is Button) return (Button)element;
            element = element.parent;
        }
        return null;
    }

    private int FindBuildingIndexFromButton(Button button){
        return int.Parse(button.name.Split("Button")[1])-1;
    }


    private void UpdateBuildingAvailabilities(){
        for(int i = 0; i < buildingButtons.Count; i++){
            int buildingIndex = i;
            Button button = buildingButtons[i];
            if(controller.BuildingCanBePurchased(buildingIndex)){
                button.style.backgroundColor = Color.green;
            }
            else{
                button.style.backgroundColor = Color.red;
            }
        }
    }

    private void UpdateResourcesUI(){
        goldLabel.text = controller.GetGold().ToString();
        gemLabel.text = controller.GetGem().ToString();
    }

    private void ResetTileColors(){
        for(int x = tilemap.cellBounds.min.x; x < tilemap.cellBounds.max.x; x++){
            for(int y = tilemap.cellBounds.min.y; y < tilemap.cellBounds.max.y; y++){
                tilemap.SetColor(new Vector3Int(x,y,0), Color.white);
            }
        }
    }

    public void SetBuildingTiles(){
        foreach(Building building in controller.GetBuildings()){
            List<Vector3Int> buildingShapeMatrix = CalculateVectorsFromOffsets(
                building.GetPosition(),
                controller.GetBuildingShapeOffsetMatrixOfShape(building.GetBuildingShape()));
            foreach(Vector3Int tileVector in buildingShapeMatrix){
                tilemap.SetTile(tileVector, buildingTile);
                occupiedTilesMatrix.Add(tileVector);
            }
        }
    }

    private void HandleDragging(){
        canBuild = false;
        if(isDragging && controller.BuildingCanBePurchased(selectedBuildingIndex)){
            canBuild = true;
            ResetTileColors();
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tilePosition = tilemap.WorldToCell(cursorPos);
            TileBase tile = tilemap.GetTile(tilePosition);
            shapeMatrix = CalculateVectorsFromOffsets(
                tilePosition,
                controller.GetBuildingShapeOffsetMatrix(selectedBuildingIndex));
            
            foreach(Vector3Int tileVector in shapeMatrix){
                if(! tilemap.GetTile(tileVector) || occupiedTilesMatrix.Contains(tileVector)) canBuild = false;
            }
            foreach(Vector3Int tileVector in shapeMatrix){
                if(canBuild) AssignMaterialToTile(tileVector, greenTileMaterial);
                else AssignMaterialToTile(tileVector,redTileMaterial);
            }
        }
    }  

    private List<Vector3Int> CalculateVectorsFromOffsets(Vector3Int tilePosition, List<Vector3Int> shapeOffsetMatrix){
        List<Vector3Int> vectors = new List<Vector3Int>();
        vectors.Add(tilePosition);
        foreach(Vector3Int offset in shapeOffsetMatrix){
            vectors.Add(tilePosition + offset);
        }
        return vectors;
    }

    private void AssignMaterialToTile(Vector3Int tilePosition, Material material){
        tilemap.SetTileFlags(tilePosition, TileFlags.None);
        tilemap.SetColor(tilePosition, material.color);
    }
    
    private void FetchUIElements(){
        VisualElement UIRoot = UIDocument.rootVisualElement;
        restartButton = UIRoot.Q<Button>("RestartButton");
        goldLabel = UIRoot.Q<Label>("ResourceGoldText");
        gemLabel = UIRoot.Q<Label>("ResourceGemText");
        buildingButtons = new List<Button>{
            UIRoot.Q<Button>("BuildingButton1"),
            UIRoot.Q<Button>("BuildingButton2"),
            UIRoot.Q<Button>("BuildingButton3"),
            UIRoot.Q<Button>("BuildingButton4"),
            UIRoot.Q<Button>("BuildingButton5"),
            UIRoot.Q<Button>("BuildingButton6")
        };
    }
}