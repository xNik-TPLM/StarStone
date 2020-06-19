using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxAmmo : MonoBehaviour
{
    public Text maxAmmo;
    private WeaponBase m_weapon;

    // Start is called before the first frame update
    void Start()
    {
        m_weapon = FindObjectOfType<WeaponBase>();
    }

    // Update is called once per frame
    void Update()
    {
        maxAmmo.text = m_weapon.MaxAmmo.ToString();
        Debug.Log(m_weapon.MaxAmmo);
    }
}
