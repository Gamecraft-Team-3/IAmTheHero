using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInputManager = Input.PlayerInputManager;

public class PlayerMovement : MonoBehaviour
{
    private enum PlayerState
    {
        Idle,
        Moving
    }

    [Header("Technical Values")]
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Vector3 playerMoveVector;
    [SerializeField] private Vector3 playerMoveIn;
    [SerializeField] private Vector3 miniPos1, miniPos2;
    [SerializeField] private Vector3 mousePos;

    [Header("Objects")] 
    [SerializeField] private PlayerInputManager playerInput;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform mini1, mini2, activeBody;
    [SerializeField] private Camera mainCamera;
    
    [Header("Fields")] 
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerMoveDamp;
    [SerializeField] private float miniPosMagnitude;
    [SerializeField] private float bobbleSpeed, bobbleMagnitude;
    [SerializeField] private LayerMask groundMask;

    private void Awake()
    {
        mainCamera.transform.parent = null;
    }


    private void Update()
    {
        playerMoveIn = new Vector3(playerInput.GetMovementVector().x, 0.0f, playerInput.GetMovementVector().y);
        playerMoveVector = Vector3.Lerp(playerMoveVector, playerMoveIn, playerMoveDamp * Time.deltaTime);

        Vector2 mouseIn = playerInput.GetMousePosition();

        Ray ray = mainCamera.ScreenPointToRay(mouseIn);
        
        if (Physics.Raycast(ray, out RaycastHit hitPoint, Mathf.Infinity, groundMask))
        {
            mousePos = hitPoint.point;
        }
        
        playerState = playerMoveIn.magnitude > 0 ? PlayerState.Moving : PlayerState.Idle;

        if (playerState != PlayerState.Idle)
        {
            Vector3 miniPos = transform.position + (playerMoveIn * -miniPosMagnitude) + new Vector3(0.0f, 0.6f, 0.0f);
            
            miniPos1 = Vector3.Lerp(miniPos1, miniPos, Time.deltaTime * 5.0f);

            miniPos = transform.position + (playerMoveIn * (-miniPosMagnitude * 2.0f)) + new Vector3(0.0f, 0.6f, 0.0f);
            
            miniPos2 = Vector3.Lerp(miniPos2, miniPos, Time.deltaTime * 2.5f);
            
            activeBody.localPosition = new Vector3(0, 1.0f + (Mathf.Sin(Time.time * bobbleSpeed) * bobbleMagnitude), 0);
            
            //activeBody.LookAt(transform.position + new Vector3(playerMoveVector.x, activeBody.position.y, playerMoveVector.z));
        }
        
        mini1.LookAt(new Vector3(activeBody.position.x, 0.6f, activeBody.position.z));
        mini2.LookAt(new Vector3(mini1.position.x, 0.6f, mini1.position.z));

        if (Vector3.Distance(mousePos + new Vector3(0.0f, 1.0f, 0.0f), activeBody.transform.position) > 0.5f)
        {
            activeBody.LookAt(mousePos + new Vector3(0.0f, activeBody.transform.position.y, 0.0f));
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = playerMoveVector * (playerSpeed * 100.0f * Time.fixedDeltaTime);
        
        mini1.position = miniPos1;
        mini2.position = miniPos2;
    }
}
