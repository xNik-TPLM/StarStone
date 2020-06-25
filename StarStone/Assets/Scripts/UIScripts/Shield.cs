using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public static bool shieldActive;
    //public Slider shieldSlider;
    public Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GetComponent<Slider>().value > 0)
            {
                GetComponent<Slider>().value -= 0.1f;
            }
            else
            {
                healthSlider.value -= 0.1f;
            }
            
            
        }
        if (shieldActive == true)
        {
            
            gameObject.SetActive(false);
        }
    }
}
