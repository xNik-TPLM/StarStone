using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractStarStone : MonoBehaviour
{
    public static int StarStoneID;
    public static int WeaponID;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider trigger)
    {
        if (trigger.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                switch (gameObject.name)
                {
                    case "EarthStarStone":
                        StarStoneID = 1;
                        WeaponID = 1;
                        FindObjectOfType<WeaponsSelect>().SetWeapon();
                        break;

                    case "FireStarStone":
                        StarStoneID = 2;
                        WeaponID = 1;
                        FindObjectOfType<WeaponsSelect>().SetWeapon();
                        break;
                }
            }

        }
    }


}
