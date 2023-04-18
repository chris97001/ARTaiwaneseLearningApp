using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuListController : MonoBehaviour
{
    private bool active = false;
    private Vector3 oriPosition;
    public RectTransform box;
    // Start is called before the first frame update

    private void Awake() {
        oriPosition = box.anchoredPosition3D;
    }

    public void toggleMenu(){
        active = !active;
        if(active == true){
            gameObject.SetActive(active);
            box.localPosition = new Vector2(oriPosition.x, -Screen.height);
            box.LeanMoveLocalY(oriPosition.y, 0.5f).setEaseOutExpo().delay = 0.1f;
        }
        else{
            box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(onComplete);
        }
    }

    private void onComplete(){
        gameObject.SetActive(active);
    }
}
