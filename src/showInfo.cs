using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showInfo : MonoBehaviour
{
    public GameObject ARCamera;
    public GameObject ARSessionOrigin;
    public Text CameraPositionText;
    public Text ARSessionOriginPositionText;
    private void Update() {
        var pos1 = ARCamera.transform.position;
        var pos2 = ARSessionOrigin.transform.position;
        CameraPositionText.text = string.Format("Camera x:{0:00.00} y:{1:00.00} z:{0:00.00}", pos1.x, pos1.y, pos1.z);
        ARSessionOriginPositionText.text = string.Format("ARSession x:{0:00.00} y:{1:00.00} z:{0:00.00}", pos2.x, pos2.y, pos2.z);
    }
}
