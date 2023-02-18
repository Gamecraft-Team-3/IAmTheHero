using System;
using Input;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager playerInput;
        [SerializeField] private PlayerManager playerManager;
        
        private void Start()
        {
            playerInput.OnAttackAction += Attack;
        }

        private void Attack(object sender, EventArgs e)
        {

        }
    }
}