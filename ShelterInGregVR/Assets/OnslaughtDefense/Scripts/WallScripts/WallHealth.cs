using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int curHealth;

    //public int repairAmount = 15;

    public Mesh fullHealthMesh;
    public Mesh twoThirdsMesh;
    public Mesh oneThirdMesh;
    public Mesh brokenMesh;

    private MeshFilter meshFilter;
    private WallState wallState;

    void Start()
    {
        curHealth = maxHealth;
        meshFilter = GetComponent<MeshFilter>();
        UpdateWallState();
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth); // Prevent negative health
        UpdateWallState();
    }

    public void RepairWall(int repairAmount)
    {
        curHealth += repairAmount;
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth); // Cap health at max
        UpdateWallState();
    }

    private void UpdateWallState()
    {
        WallState newState = GetWallState();

        if (newState != wallState)
        {
            wallState = newState;
            SwapMesh();
        }
    }

    private WallState GetWallState()
    {
        if (curHealth >= maxHealth * 0.67f)
            return WallState.FullHealth;
        else if (curHealth >= maxHealth * 0.34f)
            return WallState.TwoThirdsHealth;
        else if (curHealth > 0)
            return WallState.OneThirdHealth;
        else
            return WallState.Broken;
    }

    private void SwapMesh()
    {
        switch (wallState)
        {
            case WallState.FullHealth:
                meshFilter.mesh = fullHealthMesh;
                break;
            case WallState.TwoThirdsHealth:
                meshFilter.mesh = twoThirdsMesh;
                break;
            case WallState.OneThirdHealth:
                meshFilter.mesh = oneThirdMesh;
                break;
            case WallState.Broken:
                meshFilter.mesh = brokenMesh;
                break;
        }
    }
}
