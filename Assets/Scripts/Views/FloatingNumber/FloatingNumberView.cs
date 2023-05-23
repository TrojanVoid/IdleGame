using UnityEngine;

public class FloatingNumberView : MonoBehaviour{

    public RectTransform rect;

    public void Update(){
        rect.position += new Vector3(0f, 0.003f, 0f);
    }
}