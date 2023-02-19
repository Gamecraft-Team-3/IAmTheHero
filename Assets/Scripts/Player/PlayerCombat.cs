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
        [SerializeField] private AudioSource knightSwing, mageFire, mageExplode, arrowShoot;
        [SerializeField] private KnightCrosshair knightCrosshair;
        [SerializeField] private MageCrosshair mageCrosshair;
        [SerializeField] private RangerCrosshair rangerCrosshair;

        [Header("Cooldowns")]
        [SerializeField] private bool doTimer;
        [SerializeField] private float knightCooldown, mageCooldown, rangerCooldown;
        [SerializeField] private float cooldownTimer = 0.0f;
        [SerializeField] private float heavyTimer;
        [SerializeField] private float knightHeavyTime, mageHeavyTime, rangerHeavyTimer;
        
        [Header("Mage")] 
        [SerializeField] private GameObject mageAttackPrefab, heavyMageAttack;
        [SerializeField] private GameObject implode, explode;

        [Header("Ranger")] 
        [SerializeField] private GameObject arrowPrefab, arrowHeavy;

        [Header("Knight")] 
        [SerializeField] private GameObject swordPrefab, swordHeavy;
        public Vector3 swingPosition;
        public Quaternion swingRotation;
        
        private void Start()
        {
            playerInput.OnAttackAction += AttackBegin;
            playerInput.OnAttackReleaseAction += Attack;
        }

        private void Update()
        {
            cooldownTimer += Time.deltaTime;
            heavyTimer += doTimer ? Time.deltaTime : 0.0f;

            mageCrosshair.amplitude = heavyTimer;
            rangerCrosshair.amplitude = heavyTimer;
            knightCrosshair.amplitude = heavyTimer;
        }

        private void Attack(object sender, EventArgs e)
        {
            if (playerManager.GetHeroType() == PlayerManager.HeroType.Knight)
                KnightAttack();

            if (playerManager.GetHeroType() == PlayerManager.HeroType.Mage)
                StartCoroutine(MageAttack());

            if (playerManager.GetHeroType() == PlayerManager.HeroType.Ranger)
                RangerAttack();

            doTimer = false;
        }

        private void AttackBegin(object sender, EventArgs e)
        {
            doTimer = true;
        }

        private void KnightAttack()
        {
            if (cooldownTimer < knightCooldown)
                return;
            
            GameObject swordInstance = Instantiate(heavyTimer >= knightHeavyTime ? swordHeavy : swordPrefab, swingPosition, swingRotation);
            swordInstance.transform.parent = transform;

            cooldownTimer = 0.0f;

            heavyTimer = 0.0f;

            knightSwing.Play();
        }

        private IEnumerator MageAttack()
        {
            if (cooldownTimer < mageCooldown)
                yield break;
            
            Vector3 position = GetMouseRay();
            GameObject attackInstance = Instantiate(heavyTimer >= mageHeavyTime ? heavyMageAttack : mageAttackPrefab, position, Quaternion.identity);
            Instantiate(implode, attackInstance.transform);
            
            cooldownTimer = 0.0f;

            mageFire.Play();
            
            yield return new WaitForSeconds(0.75f);
            
            cameraController.ShakeCamera(1.0f, 32.0f, 0.1f);

            GameObject explosionInstance = Instantiate(explode, attackInstance.transform);

            if (heavyTimer >= mageHeavyTime)
                explosionInstance.transform.localScale = new Vector3(2f, 2f, 2f);
            
            heavyTimer = 0.0f;
            
            mageExplode.Play();
        }
        
        private void RangerAttack()
        {
            if (cooldownTimer < rangerCooldown)
                return;
            
            GameObject arrow = Instantiate(heavyTimer >= rangerHeavyTimer ? arrowHeavy : arrowPrefab, new Vector3(transform.position.x, 2.0f, transform.position.z), Quaternion.identity);
            
            arrow.transform.LookAt(new Vector3(GetMouseRay().x, 2.0f, GetMouseRay().z));

            cooldownTimer = 0.0f;

            heavyTimer = 0.0f;

            arrowShoot.Play();
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