using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractStarStone : MonoBehaviour
{
    private string StarStoneName;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                StarStoneName = gameObject.name;
                GetComponent<WeaponBase>().SetProjectile(StarStoneName);
            }

        }
    }


}
