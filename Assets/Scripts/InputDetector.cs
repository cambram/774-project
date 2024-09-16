using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using TMPro;

public class InputDetector : MonoBehaviour
{
    private InputData _inputData;

    // Start is called before the first frame update
    void Start()
    {
        _inputData = GetComponent<InputData>();
        Debug.Log("Started inputData: " + _inputData);
    }

    // Update is called once per frame
    void Update()
    {
        if(_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool Abutton)) {
            Debug.Log("A button clicked: " + Abutton);
        }
    }
}
