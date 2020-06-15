using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.meleeActive == true)
        {
            GetComponent<BoxCollider>().enabled = true;
            transform.Translate(Vector3.forward * attackSpeed * Time.deltaTime);
        }
        if (transform.position.x < -0.02)
        {
            PlayerController.meleeActive = false;
        }
    }

    
}
