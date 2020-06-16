using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackSpeed;
    public Transform start;
    public float end;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        start.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.meleeActive == true)
        {
            GetComponent<BoxCollider>().enabled = true;
            transform.position = Mathf.MoveTowards(transform.position.x, end, Time.deltaTime * attackSpeed);
            PlayerController.meleeActive = false;
        }
        else
        {
            transform.position = start.position;
        }
    }
}
