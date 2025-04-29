using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class LeftController : MonoBehaviour
{
    public string wallTag = "Wall";

    public ActionBasedController controller;
    public InputActionReference repairAction;
    public XRRayInteractor rayInteractor;

    void Start()
    {
        if (rayInteractor == null && controller != null)
        {
            rayInteractor = controller.GetComponent<XRRayInteractor>();
        }
    }

    void OnButtonDown()
    {
        if (rayInteractor != null && rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (hit.collider.CompareTag(wallTag)) // Ensure we hit a wall
            {
                WallHealth wallHealth = hit.collider.GetComponent<WallHealth>();
                if (wallHealth != null)
                {
                    RepairWall(wallHealth);
                }
            }
        }
    }

    void RepairWall(WallHealth wall)
    {
        wall.RepairWall(20); // Repairs 20 health per action
    }
}
