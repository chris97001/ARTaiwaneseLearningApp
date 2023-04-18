using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{
    public GameObject objectToPlace;
    public ARRaycastManager rayCastManager;
    public TextMesh AnimalNameText;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Update is called once per frame
    void Update()
    {
        touch_Drag();
    }
    void touch_Drag()
    {
        if (Input.touchCount > 0)
        {
            var touchPosition = Input.GetTouch(0).position;
            if(rayCastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon)){
                var hitPose = hits[0].pose;
                objectToPlace.transform.position = hitPose.position;
                objectToPlace.transform.rotation = hitPose.rotation;
            }
        }
    }

    public void updateObjectToPlace(GameObject obj){
        objectToPlace = obj;
    }
}
