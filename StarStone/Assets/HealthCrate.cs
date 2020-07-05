using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCrate : MonoBehaviour
{
    private PlayerController m_player;
    public int healthCrateValue;
    // Start is called before the first frame update
    void Start()
    {
        m_player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            m_player.currentHealth += healthCrateValue;
            gameObject.SetActive(false);
        }
    }
}
