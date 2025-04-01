using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    public Animator anim;
    public bool open = false;

    public ActionBasedController controller;
    public InputActionReference interactAction;
    public XRRayInteractor rayInteractor;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (rayInteractor == null && controller != null)
        {
            rayInteractor = controller.GetComponent<XRRayInteractor>();
        }
    }

    void OnButtonDown()
    {
        if (rayInteractor != null && rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (!open)
            {
                anim.Play("DoorOpen", 0, 0f);
                open = true;
            }

            else if (open)
            {
                anim.Play("DoorClose", 0, 0f);
                open = false;
            }
        }
    }
}
