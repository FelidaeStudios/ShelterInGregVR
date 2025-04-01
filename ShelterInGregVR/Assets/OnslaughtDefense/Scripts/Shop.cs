using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint smallTurret;
    public TurretBlueprint largeTurret;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard feesh selected");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void PurchaseSmallTurret()
    {
        Debug.Log("Small feesh selected");
        buildManager.SetTurretToBuild(buildManager.smallTurretPrefab);
    }

    public void PurchaseLargeTurret()
    {
        Debug.Log("Large feesh selected");
        buildManager.SetTurretToBuild(buildManager.largeTurretPrefab);
    }
}
