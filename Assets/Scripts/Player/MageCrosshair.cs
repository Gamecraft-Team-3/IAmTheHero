using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class MageCrosshair : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInput;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rotationSpeed, cursorSpeed;
    [SerializeField] private LayerMask groundMask;

    void Update()
    {
        Vector2 mouseIn = playerInput.GetMousePosition();

        Ray ray = mainCamera.ScreenPointToRay(mouseIn);
        
        if (Physics.Raycast(ray, out RaycastHit hitPoint, Mathf.Infinity, groundMask))
        {
            Vector3 mousePos = hitPoint.point;

            transform.position = Vector3.Lerp(transform.position,mousePos + new Vector3(0.0f, 0.325f, 0.0f), cursorSpeed * Time.deltaTime);
        }
        
        transform.Rotate(new Vector3(0.0f, rotationSpeed * Time.deltaTime, 0.0f));
    }
}
