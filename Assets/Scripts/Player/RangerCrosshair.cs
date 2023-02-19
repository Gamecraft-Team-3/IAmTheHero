using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class RangerCrosshair : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInput;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float cursorSpeed;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform visual;
    public float amplitude;

    void Update()
    {
        Vector2 mouseIn = playerInput.GetMousePosition();

        Ray ray = mainCamera.ScreenPointToRay(mouseIn);
        
        if (Physics.Raycast(ray, out RaycastHit hitPoint, Mathf.Infinity, groundMask))
        {
            Vector3 mousePos = hitPoint.point;

            Vector3 midPoint = Vector3.Lerp(playerTransform.position, mousePos, 0.25f);

            midPoint.y = 1.0f;
            
            transform.position = Vector3.Lerp(transform.position, midPoint, cursorSpeed * Time.deltaTime);

            transform.LookAt(new Vector3(mousePos.x, 1.0f, mousePos.z));
            
            float scaleXY = Vector3.Distance(playerTransform.position, mousePos) / 10.0f;
            scaleXY = Mathf.Clamp(scaleXY, 0.5f, 0.75f);
                
            float scaleZ = Vector3.Distance(playerTransform.position, mousePos) / 10.0f;

            scaleZ = Mathf.Clamp(scaleZ, 0.2f, 1.35f);
            
            transform.localScale = new Vector3(scaleXY, scaleXY, scaleZ);
        }

        if (amplitude >= 1.5f)
            visual.transform.Rotate(Vector3.forward * (600.0f * Time.deltaTime));
        else
            visual.transform.localRotation = Quaternion.Euler(Vector3.zero);
            
        
    }
}
