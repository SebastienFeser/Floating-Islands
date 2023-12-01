using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsManager : MonoBehaviour
{
    DraggablePlatform[] draggablePlatforms;
    private void Start()
    {
        draggablePlatforms = GetComponentsInChildren<DraggablePlatform>();
    }

    public void DeactivateAllSelectedPlatforms()
    {
        foreach (var platform in draggablePlatforms)
        {
            platform.Deselect();
        }
    }
}
