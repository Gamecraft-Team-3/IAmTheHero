using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInput;

    [SerializeField] private GameObject knight, mage, ranger;
    [SerializeField] private Transform active, mini1, mini2;

    [SerializeField] private ParticleSystem dustCloud;

    [SerializeField] private GameObject mCross, rCross, kCross;
    [SerializeField] private GameObject crosshairs;
    
    private enum HeroType
    {
        Knight,
        Mage,
        Ranger
    }

    [SerializeField] private HeroType heroType;

    private void Start()
    {
        heroType = HeroType.Knight;
        kCross.SetActive(true);

        crosshairs.transform.parent = null;

        playerInput.OnInteractAction += InvokeSwitchCharacter;
    }

    private void InvokeSwitchCharacter(object o, EventArgs e)
    {
        Invoke(nameof(SwitchCharacter), 0.3f);
        
        dustCloud.Play();
        
        playerInput.SetInputState(false);
    }

    private void SwitchCharacter()
    {
        if (heroType == HeroType.Knight)
        {
            knight.transform.parent = mini2;
            ranger.transform.parent = mini1;
            mage.transform.parent = active;
            
            kCross.SetActive(false);
            mCross.SetActive(true);

            heroType = HeroType.Mage;
            
            ResetTransform();
            
            return;
        }

        if (heroType == HeroType.Mage)
        {
            mage.transform.parent = mini2;
            knight.transform.parent = mini1;
            ranger.transform.parent = active;

            mCross.SetActive(false);
            rCross.SetActive(true);
            
            heroType = HeroType.Ranger;
            
            ResetTransform();
            
            return;
        }
        
        if (heroType == HeroType.Ranger)
        {
            ranger.transform.parent = mini2;
            mage.transform.parent = mini1;
            knight.transform.parent = active;
            
            rCross.SetActive(false);
            kCross.SetActive(true);

            heroType = HeroType.Knight;
            
            ResetTransform();
            
            return;
        }
    }

    private void ResetTransform()
    {
        knight.transform.localPosition = Vector3.zero;
        knight.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        knight.transform.localRotation = Quaternion.Euler(Vector3.zero);
        
        mage.transform.localPosition = Vector3.zero;
        mage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        mage.transform.localRotation = Quaternion.Euler(Vector3.zero);
        
        ranger.transform.localPosition = Vector3.zero;
        ranger.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        ranger.transform.localRotation = Quaternion.Euler(Vector3.zero);
        
        playerInput.SetInputState(true);
    }
}
