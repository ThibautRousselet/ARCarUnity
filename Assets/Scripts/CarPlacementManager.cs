using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;

//Handles the placement of the car when pressing the switch button
public class CarPlacementManager : MonoBehaviour
{
    private ARSessionOrigin sessionOrigin;
    public GameObject Car;
    public bool IsInPlacementMode = true;
    private bool isPlacementPoseValid;
    private Pose placementPose;
    private UIManager uiManager;

    void Start()
    {
        sessionOrigin = FindObjectOfType<ARSessionOrigin>();
        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        CheckPlacementPos();
        UpdateDisplay();
        CheckOutOfBounds();
    }

    private void CheckPlacementPos()
    {
        if (IsInPlacementMode)
        {
            //Raycast in screen center
            Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            sessionOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

            //If raycast hits plane placementPose is valid
            if (hits.Count > 0)
            {
                isPlacementPoseValid = true;
                placementPose = hits[0].pose;
            }
            else
                isPlacementPoseValid = false;
        }
    }

    private void UpdateDisplay()
    {
        if (IsInPlacementMode)
        {
            if (isPlacementPoseValid)
            {
                //The car is showed above its spawn position if it s valid
                Car.SetActive(true);
                Car.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            }
            else
                Car.SetActive(false);
        }
    }

    public void SwitchMode()
    {
        //Switch between placement and gaming mode
        if (IsInPlacementMode)
        {
            if (isPlacementPoseValid)
                IsInPlacementMode = false;
        }
        else
        {
            IsInPlacementMode = true;
        }
        Car.GetComponentInChildren<TrailRenderer>().emitting = !IsInPlacementMode;
        uiManager.UpdateUI();
    }

    //Back to placement mode if the car falls in the void
    private void CheckOutOfBounds()
    {
        if (Car.transform.position.y < -50 && !IsInPlacementMode)
        {
            SwitchMode();
        }
    }
}
