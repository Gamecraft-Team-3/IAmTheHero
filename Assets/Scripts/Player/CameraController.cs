using System;
using Input;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Vector3 basePosition, currentPosition;
        [SerializeField] private float shakeSpeed, shakeMagnitude, shakeTime, shakeTimer;
        [SerializeField] private bool isShaking;
        
        [SerializeField] private PlayerInputManager playerInput;
        [SerializeField] private LayerMask groundMask;


        private void Update()
        {
            
            Vector2 mouseIn = playerInput.GetMousePosition();

            Ray ray = mainCamera.ScreenPointToRay(mouseIn);
        
            if (Physics.Raycast(ray, out RaycastHit hitPoint, Mathf.Infinity, groundMask))
            {
                Vector3 mousePos = hitPoint.point;

                Vector3 midPoint = Vector3.Lerp(Vector3.zero, mousePos, 0.035f);

                midPoint.y = 1.0f;

                //Vector3.ClampMagnitude(midPoint, 0.3f);
            
                currentPosition = Vector3.Lerp(currentPosition, midPoint, 12.0f * Time.deltaTime);
            
                Vector3 direction = (Vector3.zero - mousePos).normalized;
            
                var angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            }

            
            if (shakeTimer <= shakeTime && isShaking)
                shakeTimer += Time.deltaTime;
            else
                isShaking = false;

            if (isShaking)
            {
                float sine = (Mathf.Sin(Time.time * shakeSpeed) * shakeMagnitude);
                transform.position = basePosition + (((transform.up + transform.right) * sine));
            }
            else
                transform.position = Vector3.Lerp(transform.position,
                    new Vector3(currentPosition.x, basePosition.y, currentPosition.z + basePosition.z),
                        16.0f * Time.deltaTime);
        }

        public void ShakeCamera(float time, float speed, float magnitude)
        {
            shakeTime = time;
            shakeTimer = 0.0f;

            shakeSpeed = speed;
            shakeMagnitude = magnitude;
            
            isShaking = true;
        }
    }
}