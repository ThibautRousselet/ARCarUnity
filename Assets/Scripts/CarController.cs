using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controller of the car movements
public class CarController : MonoBehaviour
{
    private float torqueInput;
    private float steeringInput;
    private float steeringAngle = 0f;
    private float Torque = 0f;

    [Header("Input settings")]
    [Range(0f, 90f)]
    public float MaxSteeringAngle = 25f;
    public Joystick SteeringJoystick;

    public float MaxTorque = 100f;
    public Joystick TorqueJoystick;

    [Header("Wheel colliders")]
    public WheelCollider FLCollider;
    public WheelCollider FRCollider;
    public WheelCollider RLCollider;
    public WheelCollider RRCollider;

    [Header("Wheel transforms")]
    public Transform FLTransform;
    public Transform FRTransform;
    public Transform RLTransform;
    public Transform RRTransform;

    private void FixedUpdate()
    {
        UpdateInput();
        Steer();
        Accelerate();
        UpdateWheelRotations();
    }

    private void UpdateInput()
    {
        steeringInput = SteeringJoystick.Horizontal;
        torqueInput = TorqueJoystick.Vertical;
    }

    private void Steer()
    {
        steeringAngle = steeringInput * MaxSteeringAngle;
        FLCollider.steerAngle = steeringAngle;
        FRCollider.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        Torque = torqueInput * MaxTorque;
        RLCollider.motorTorque = Torque;
        RRCollider.motorTorque = Torque;
    }

    private void UpdateWheelRotations()
    {
        UpdateWheelRotation(FLCollider, FLTransform);
        UpdateWheelRotation(FRCollider, FRTransform);
        UpdateWheelRotation(RLCollider, RLTransform);
        UpdateWheelRotation(RRCollider, RRTransform);
    }

    //Update wheel model transform based on its collider's values
    private void UpdateWheelRotation(WheelCollider wCollider, Transform wTransform)
    {
        Vector3 position = wTransform.position;
        Quaternion rotation = wTransform.rotation;
        wCollider.GetWorldPose(out position, out rotation);
        wTransform.position = position;
        wTransform.rotation = rotation;
    }
}
