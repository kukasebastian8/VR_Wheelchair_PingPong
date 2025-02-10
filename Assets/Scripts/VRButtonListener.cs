using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRButtonListener : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Get a list of connected devices
        var devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        foreach (var device in devices)
        {
            // Check for button presses on each device
            CheckDeviceButtons(device);
        }
    }

    private void CheckDeviceButtons(InputDevice device)
    {
        // List of common input feature usages to check
        List<InputFeatureUsage<bool>> buttonUsages = new List<InputFeatureUsage<bool>>()
        {
            CommonUsages.primaryButton,
            CommonUsages.secondaryButton,
            CommonUsages.gripButton,
            CommonUsages.triggerButton,
            CommonUsages.menuButton
        };

        foreach (var usage in buttonUsages)
        {
            if (device.TryGetFeatureValue(usage, out bool pressed) && pressed)
            {
                Debug.Log($"{usage.name} pressed on device: {device.name}");
            }
        }

        // Check for joystick or touchpad clicks
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool primaryAxisClicked) && primaryAxisClicked)
        {
            Debug.Log($"Primary 2D Axis Click pressed on device: {device.name}");
        }
    }
}
