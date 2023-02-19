using System;
using System.Collections;
using Input;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private PlayerInputManager playerInput;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private CameraController cameraController;

        [Header("Cooldowns")]
        [SerializeField] private float knightCooldown, mageCooldown, rangerCooldown;
        [SerializeField] private float cooldownTimer = 0.0f;

        [Header("Mage Attack Objects")] 
        [SerializeField] private GameObject mageAttackPrefab;
        [SerializeField] private GameObject implode, explode;

        [Header("Ranger")] 
        [SerializeField] private GameObject arrowPrefab;

        [Header("Knight")] 
        [SerializeField] private GameObject swordPrefab;
        public Vector3 swingPosition;
        public Quaternion swingRotation;
        
        private void Start()
        {
            playerInput.OnAttackAction += Attack;
        }

        private void Update()
        {
            cooldownTimer += Time.deltaTime;
        }

        private void Attack(object sender, EventArgs e)
        {
            if (playerManager.GetHeroType() == PlayerManager.HeroType.Knight)
                KnightAttack();

            if (playerManager.GetHeroType() == PlayerManager.HeroType.Mage)
                StartCoroutine(MageAttack());

            if (playerManager.GetHeroType() == PlayerManager.HeroType.Ranger)
                RangerAttack();
        }

        private void KnightAttack()
        {
            if (cooldownTimer < knightCooldown)
                return;
            
            GameObject swordInstance = Instantiate(swordPrefab, swingPosition, swingRotation);
            swordInstance.transform.parent = transform;

            cooldownTimer = 0.75f;
        }

        private IEnumerator MageAttack()
        {
            if (cooldownTimer < mageCooldown)
                yield break;
            
            Vector3 position = GetMouseRay();
            Instantiate(mageAttackPrefab, position, Quaternion.identity);
            Instantiate(implode, position, Quaternion.identity);
            
            cooldownTimer = 1.5f;
            
            yield return new WaitForSeconds(1.75f);
            
            cameraController.ShakeCamera(1.0f, 32.0f, 0.1f);

            Instantiate(explode, position, Quaternion.identity);
        }
        

        private void RangerAttack()
        {
            if (cooldownTimer < rangerCooldown)
                return;
            
            GameObject arrow = Instantiate(arrowPrefab, new Vector3(transform.position.x, 2.0f, transform.position.z), Quaternion.identity);
            
            arrow.transform.LookAt(new Vector3(GetMouseRay().x, 2.0f, GetMouseRay().z));
            
            cooldownTimer = 0.0f;
        }

        private Vector3 GetMouseRay()
        {
            Vector2 mouseIn = playerInput.GetMousePosition();

            Ray ray = mainCamera.ScreenPointToRay(mouseIn);
        
            if (Physics.Raycast(ray, out RaycastHit hitPoint, Mathf.Infinity, groundMask))
                return hitPoint.point;

            return Vector3.zero;
        }
    }
}