using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    private Text m_interactText;

    // Start is called before the first frame update
    void Start()
    {
        m_interactText = GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Ladder.CanInteract == true)
        {
            m_interactText.enabled = true;
        }
        else
        {
            m_interactText.enabled = false;
        }
    }
}
