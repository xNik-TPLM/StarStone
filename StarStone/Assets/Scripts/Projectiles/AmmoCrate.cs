using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    private WeaponBase m_weaponAdd;
    // Start is called before the first frame update
    void Start()
    {
        m_weaponAdd = GetComponentInChildren<WeaponBase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_weaponAdd.MaxAmmo += 100;
            //gameObject.SetActive(false);
        }
    }
}