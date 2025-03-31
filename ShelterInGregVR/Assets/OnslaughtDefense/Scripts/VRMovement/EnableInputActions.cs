using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnableInputActions : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    void Start()
    {
        if (inputActionAsset != null)
        {
            inputActionAsset.Enable();
        }
    }
}
