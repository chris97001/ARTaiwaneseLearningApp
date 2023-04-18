using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjectButton : MonoBehaviour
{
    public MenuListController menuListController;
    public PlacementController placementController;
    private void Start() {
        menuListController = GameObject.Find("MenuGroup").GetComponent<MenuListController>();
        placementController = GameObject.Find("AR Session Origin").GetComponent<PlacementController>();
    }
    public void selectedObject(){
        placementController.SpawnAnimal(transform.name, "");
        menuListController.toggleMenu();
    }
}
