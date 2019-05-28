using UnityEngine;
using UnityEngine.UI;

public class PMov : MonoBehaviour
{
    public Transform life;
    public Transform actualLife;
    public Transform placeHolder;
    public float playerLife = 0;
    private Vector3 mousePos = Vector3.zero;
    private Vector3 textPosition = Vector3.zero;
    void Update()
    {
        //Player position corresponds with mouse world position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 2f;
        this.transform.position = mousePos;

        //UI text position corresponds with placeHolder position
        textPosition = Camera.main.WorldToScreenPoint(placeHolder.position);
        life.transform.position = textPosition;
    }

    /*void UpdateLife(){
        if(playerLife != 0f){
            float percent = actualLife.RectTransform.width / playerLife;
            actualLife.RectTransform.LocalScale += percent;
        }
    }*/
}
