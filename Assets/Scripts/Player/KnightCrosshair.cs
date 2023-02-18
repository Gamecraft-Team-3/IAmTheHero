using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Input;
using UnityEngine;

public class KnightCrosshair : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInput;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float cursorSpeed;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerCombat playerCombat;   

    void Update()
    {
        Vector2 mouseIn = playerInput.GetMousePosition();

        Ray ray = mainCamera.ScreenPointToRay(mouseIn);
        
        if (Physics.Raycast(ray, out RaycastHit hitPoint, Mathf.Infinity, groundMask))
        {
            Vector3 mousePos = hitPoint.point;

            Vector3 midPoint = Vector3.Lerp(playerTransform.position, mousePos, 0.1f);

            midPoint.y = 1.0f;

            //Vector3.ClampMagnitude(midPoint, 0.3f);
            
            transform.position = Vector3.Lerp(transform.position, midPoint, cursorSpeed * Time.deltaTime);
            
            Vector3 direction = (playerTransform.position - mousePos).normalized;
            
            var angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; 
            var offset = -90f;

            //transform.rotation = Quaternion.Euler(new Vector3(0.0f, angle + offset, 0.0f));
            
            transform.LookAt(new Vector3(mousePos.x, 1.0f, mousePos.z));

            playerCombat.swingPosition = transform.position;
            playerCombat.swingRotation = transform.rotation;
        }
    }
}
