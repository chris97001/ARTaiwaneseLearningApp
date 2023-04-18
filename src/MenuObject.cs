using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObject : MonoBehaviour
{
    private bool selected = false;
    public void selecteMenu()
    {
        selected = !selected;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if(selected)
        {
            meshRenderer.material.color = Color.red;
        }
        else
        {
            meshRenderer.material.color = Color.white;
        }
    }
}
