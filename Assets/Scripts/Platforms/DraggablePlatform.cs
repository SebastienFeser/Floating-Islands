using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggablePlatform : MonoBehaviour
{
    Vector3 mousePositionOffset;
    bool isActive;
    PlatformsManager platformsManager;

    private void Start()
    {
        platformsManager = GetComponentInParent<PlatformsManager>();
    }
    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        platformsManager.DeactivateAllSelectedPlatforms();
        isActive = true;
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }

    private void Update()
    {
        TurnGameObject();
    }

    private void TurnGameObject()
    {
        if(isActive)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 45f);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 45f);
            }
        }
    }

    public void Deselect()
    {
        isActive = false;
    }
}
