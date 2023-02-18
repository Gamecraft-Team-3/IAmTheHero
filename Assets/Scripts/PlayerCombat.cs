using System;
using Input;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager pim;
        
        private void Start()
        {
            pim.OnAttackAction += Attack;
        }

        private void Attack(object sender, EventArgs e)
        {
            
        }
    }
}