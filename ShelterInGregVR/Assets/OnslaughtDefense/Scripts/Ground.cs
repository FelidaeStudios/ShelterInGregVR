using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Ground : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;

    private Renderer rend;
    private Color startColor;

    public ActionBasedController controller;
    public InputActionReference placementAction;
    public XRRayInteractor rayInteractor;

    BuildManager buildManager;

    private List<GameObject> turrets = new List<GameObject>();

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;

        if (rayInteractor == null && controller != null)
        {
            rayInteractor = controller.GetComponent<XRRayInteractor>();
        }

        if (rayInteractor != null)
        {
            rayInteractor.hoverEntered.AddListener(OnHoverEnter);
            rayInteractor.hoverExited.AddListener(OnHoverExit);
        }

        if (placementAction != null)
        {
            Debug.Log("button pressed 1");
            placementAction.action.Enable();
            placementAction.action.performed += ctx => OnButtonDown();
            Debug.Log("button pressed 2");
        }
    }

    void OnButtonDown()
    {
        if (buildManager.GetTurretToBuild() == null || turret != null)
        {
            Debug.Log("No turret selected");
            return;
        }

        /*if (turret != null)
        {
            Debug.Log("Turret already placed");
            return;
        }

        if (rayInteractor != null && rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            Debug.Log("Turret placed");
            //Instantiate(turretPrefab, hit.point, Quaternion.identity);
            GameObject turretToBuild = buildManager.GetTurretToBuild();
            turret = (GameObject)Instantiate(turretToBuild, hit.point + positionOffset, Quaternion.identity);
            Debug.Log("Hit point: " + hit.point);
            Debug.Log("Turret placed at: " + (hit.point + positionOffset));*/
            /*if (placementAction.action.WasPressedThisFrame())
            {
                
            }*/
        //}

        if (rayInteractor != null && rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            GameObject turretToBuild = buildManager.GetTurretToBuild();
            GameObject newTurret = Instantiate(turretToBuild, hit.point + positionOffset, Quaternion.identity);
            turrets.Add(newTurret);
            Debug.Log("Turret placed at: " + (hit.point + positionOffset));
        }

        else
        {
            Debug.Log("Raycast did not hit");
        }

        //GameObject turretToBuild = buildManager.GetTurretToBuild();
        //turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    private void OnHoverEnter(HoverEnterEventArgs args) //Changes color if highlighted and eligible -> Move to pointer icon script?
    {
        if (buildManager.GetTurretToBuild() != null)
        {
            rend.material.color = hoverColor;
        }

    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        rend.material.color = startColor;
    }
}
