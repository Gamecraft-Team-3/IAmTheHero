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
    public float amplitude;

    void Update()
    {
        Vector2 mouseIn = playerInput.GetMousePosition();

        Ray ray = mainCamera.ScreenPointToRay(mouseIn);
        
        if (Physics.Raycast(ray, out RaycastHit hitPoint, Mathf.Infinity, groundMask))
        {
            Vector3 mousePos = hitPoint.point;

            transform.position = Vector3.Lerp(transform.position,mousePos + new Vector3(0.0f, 0.325f, 0.0f), cursorSpeed * Time.deltaTime);
        }
        
        float amplitudeRotation = Mathf.Clamp(amplitude * 6, 1.0f, 6.0f);

        transform.Rotate(new Vector3(0.0f, rotationSpeed * amplitudeRotation * Time.deltaTime, 0.0f));

        float amplitudeScale = Mathf.Clamp(amplitude / 1.5f, 1.25f, 2.25f);
        transform.localScale =
            Vector3.Lerp(transform.localScale, new Vector3(amplitudeScale, amplitudeScale + 0.25f, amplitudeScale), 24f * Time.deltaTime);
    }
}
