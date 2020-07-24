using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloading : MonoBehaviour
{
    Animator reloadAnimation;
    private PlayerController m_playerController;

    // Start is called before the first frame update
    void Start()
    {
        reloadAnimation = GetComponent<Animator>();
        m_playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        ReloadCheck();
    }

    public void ReloadCheck()
    {
        if (m_playerController.isReloading)
        {
            ReloadAnimation();
        }
    }

    public void ReloadAnimation()
    {
        reloadAnimation.SetBool("Reload", true);   
    }

    public void StopReloadAnimation()
    {
        if (reloadAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            reloadAnimation.SetBool("Reload", false); // This stops the animation from running once it has completed a cycle
            //isReloading = false;
        }
    }
}
