using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PointerMoveTo : MonoBehaviour
{
    public GameObject ground;
    public XRRayInteractor rayInteractor;

    void Update()
    {
        if (rayInteractor == null)
        {
            return;
        }

        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            if (hit.collider.gameObject == ground)
            {
                transform.position = hit.point;
            }
        }
    }
}
