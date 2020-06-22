using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public Text ammoDisplay; // This displays the current ammo left in the clip
    public Text maxAmmo; // This displays the maximum ammo that is left that the player has

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = WeaponBase.CurrentAmmo.ToString();
        maxAmmo.text = GetComponentInChildren<WeaponBase>().MaxAmmo.ToString();
    }
}
