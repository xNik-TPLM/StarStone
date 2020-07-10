using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAlters : MonoBehaviour
{
    public static bool HasSigilInteracted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Interact") && WaveSystem.InIntermission == true)
            {
                HasSigilInteracted = true;
            }
        }
    }
}
