using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

//Hides joysticks in placement mode and update switchButton text
public class UIManager : MonoBehaviour
{
    public GameObject SteeringJoystick;
    public GameObject TorqueJoystick;
    public Text SwitchModeButtonText;
    private CarPlacementManager CarManager;
    void Start()
    {
        CarManager = FindObjectOfType<CarPlacementManager>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (CarManager.IsInPlacementMode)
        {
            SteeringJoystick.SetActive(false);
            TorqueJoystick.SetActive(false);
            SwitchModeButtonText.text = "Spawn";
        }
        else
        {
            SteeringJoystick.SetActive(true);
            TorqueJoystick.SetActive(true);
            SwitchModeButtonText.text = "Reset";
        }
    }
}
