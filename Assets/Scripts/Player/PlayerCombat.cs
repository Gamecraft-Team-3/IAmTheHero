using System;
using System.Collections;
using Input;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager playerInput;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask groundMask;

        [SerializeField] private float knightCooldown, mageCooldown, rangerCooldown;

        [Header("Mage Attack Objects")] 
        [SerializeField] private GameObject mageAttackPrefab;
        [SerializeField] private GameObject implode, explode;
        
        private void Start()
        {
            playerInput.OnAttackAction += Attack;
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
            
        }

        private IEnumerator MageAttack()
        {
            Vector3 position = GetMouseRay();
            Instantiate(mageAttackPrefab, position, Quaternion.identity);
            Instantiate(implode, position, Quaternion.identity);
            
            yield return new WaitForSeconds(1.75f);

            Instantiate(explode, position, Quaternion.identity);
        }
        

        private void RangerAttack()
        {
            
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