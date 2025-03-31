using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    //public ActionBasedController controller;
    //public InputActionReference selectionAction;

    public GameObject standardTurretPrefab;
    public GameObject smallTurretPrefab;

    private GameObject turretToBuild;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager is active in this scene.");
            return;
        }

        instance = this;
    }

    void Start()
    {
        turretToBuild = null;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
        Debug.Log("Turret selected: " + turret.name);
    }
}
