using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using Lean.Touch;

public class PlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject placedPrefab;
    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    private GameObject placedObject;
    private Vector2 touchPosition = default;
    [SerializeField]
    private ARRaycastManager arRaycastManager;
    [SerializeField]
    private ARPlaneManager arPlaneManager;
    private bool onTouchHold = false;
    [SerializeField]
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // public Text debugText_click;
    // public Text debugText_hold;
    private bool isTouchEnable;
    public GameObject switch_toggle;
    public Sprite switch_on;
    public Sprite switch_off;
    private bool isFirstTouch = true;
    [SerializeField]
    private GameObject LeanTouch;

    public Text NameText;

    private void Awake()
    {
        // debugText_click = GameObject.Find("debug_click_text").GetComponent<Text>();
        // debugText_hold = GameObject.Find("debug_hold_text").GetComponent<Text>();
        isTouchEnable = true;
        switch_toggle.GetComponent<Image>().sprite = switch_on;
        arRaycastManager = GetComponent<ARRaycastManager>();
        placedObject = Instantiate(placedPrefab, Vector3.forward, Quaternion.Euler(0f, 0f, 0f));
        placedObject.transform.Rotate(0, 180, 0);
        NameText = placedObject.transform.Find("Canvas").transform.Find("Label").transform.Find("LabelButton").transform.Find("Text").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        touch_drag();
        foreach (var plane in arPlaneManager.trackables)
        {
            if (isTouchEnable)
            {
                plane.gameObject.SetActive(true);
            }
            else
            {
                plane.gameObject.SetActive(false);
            }
        }
    }


    private void touch_drag()
    {
        // debugText_click.text = string.Format("Clicked:finger {0}, TouchEnable:{1}", Input.touchCount.ToString(), isTouchEnable.ToString());
        if (isTouchEnable == false) return;
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.transform.tag == "Spawning")
                    {
                        placedObject = hitObject.transform.root.gameObject;
                        // debugText_click.text = string.Format("Clicked:{0}",placedObject.name);
                        onTouchHold = true;
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                onTouchHold = false;
            }
        }
        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            if (isFirstTouch == true)
            {
                isFirstTouch = false;
                // placedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                // placedObject.transform.Find("ar_cursor").gameObject.SetActive(true);
                placedObject.transform.position = hitPose.position;
                placedObject.transform.rotation = hitPose.rotation;
                placedObject.transform.Rotate(0, 180, 0);
            }
            else
            {
                if (onTouchHold)
                {
                    // debugText_hold.text = "holding";
                    placedObject.transform.position = hitPose.position;
                }
                else
                {
                    // debugText_hold.text = "not holding";
                }
            }
        }
    }

    public void disableTouch()
    {
        isTouchEnable = false;
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
        LeanTouch.SetActive(false);
    }
    public void enableTouch()
    {
        isTouchEnable = true;
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
        LeanTouch.SetActive(true);
    }

    public void SpawnAnimal(string animalName, string animalName_ch)
    {
        for (int i = 0; i < placedObject.transform.childCount; i++)
        {
            if (placedObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                placedObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            if (placedObject.transform.GetChild(i).name == animalName)
            {
                placedObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        placedObject.transform.Find("Canvas").gameObject.SetActive(true);
        NameText.text = animalName_ch;
    }

    public void cleanAnimal()
    {
        for (int i = 0; i < placedObject.transform.childCount; i++)
        {
            placedObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void onclick()
    {
        if (isTouchEnable)
        {
            disableTouch();
            switch_toggle.GetComponent<Image>().sprite = switch_off;
        }
        else
        {
            enableTouch();
            switch_toggle.GetComponent<Image>().sprite = switch_on;
        }

    }
}
