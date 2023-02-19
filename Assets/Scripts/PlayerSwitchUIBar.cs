using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSwitchUIBar : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager = null;
    [SerializeField] private Image thisImage = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //thisImage.fillAmount = playerManager.GetTimeRemaining();
    }
}
