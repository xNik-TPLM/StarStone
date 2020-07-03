using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int m_sliderColourIndex;

    public float MaxGeneratorTemperature;
    public int TemperatureStatesAmount;

    public Vector3[] SliderColours;

    public Text TimerText;
    public Text OverheatingText;
    public Slider GeneratorTemperatureSlider;
    public Image TemperatureFill;

    

    // Start is called before the first frame update
    void Start()
    {
        GeneratorTemperatureSlider.maxValue = MaxGeneratorTemperature;
        GeneratorTemperatureSlider.value = WaveSystem.GeneratorTemperature;

        OverheatingText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        UpdateGeneratorSlider();
    }

    private void UpdateTimer()
    {
        string timerSeconds = ((int)WaveSystem.WaveTimer % 60).ToString("00");
        string timerMinutes = ((int)WaveSystem.WaveTimer / 60).ToString("00");
        
        TimerText.text = "Time: " + timerMinutes + ":" + timerSeconds;
    }

    private void UpdateGeneratorSlider()
    {
        GeneratorTemperatureSlider.value = WaveSystem.GeneratorTemperature;

        switch (m_sliderColourIndex)
        {
            case 0:              
                if(WaveSystem.GeneratorTemperature == Mathf.Clamp(WaveSystem.GeneratorTemperature, (MaxGeneratorTemperature / TemperatureStatesAmount), ((MaxGeneratorTemperature / TemperatureStatesAmount) * 2)))
                {
                    m_sliderColourIndex = 1;
                }
                break;

            case 1:
                if (WaveSystem.GeneratorTemperature == Mathf.Clamp(WaveSystem.GeneratorTemperature, ((MaxGeneratorTemperature / TemperatureStatesAmount) * 2), MaxGeneratorTemperature -1))
                {
                    m_sliderColourIndex = 2;
                }
                else if(WaveSystem.GeneratorTemperature < (MaxGeneratorTemperature / TemperatureStatesAmount))
                {
                    m_sliderColourIndex = 0;
                }
                break;

            case 2:
                if(WaveSystem.GeneratorTemperature == MaxGeneratorTemperature)
                {
                    m_sliderColourIndex = 3;
                }
                else if(WaveSystem.GeneratorTemperature == Mathf.Clamp(WaveSystem.GeneratorTemperature, (MaxGeneratorTemperature / TemperatureStatesAmount), ((MaxGeneratorTemperature / TemperatureStatesAmount) * 2)))
                {
                    m_sliderColourIndex = 1;
                }
                break;

            case 3:
                WaveSystem.IsGeneratorOverheating = true;
                OverheatingText.enabled = true;

                if(WaveSystem.GeneratorTemperature == Mathf.Clamp(WaveSystem.GeneratorTemperature, ((MaxGeneratorTemperature / TemperatureStatesAmount) * 2), MaxGeneratorTemperature -1))
                {
                    m_sliderColourIndex = 2;
                    WaveSystem.IsGeneratorOverheating = false;
                    OverheatingText.enabled = false;
                }
                break;
        }

        TemperatureFill.color = new Color(SliderColours[m_sliderColourIndex].x, SliderColours[m_sliderColourIndex].y, SliderColours[m_sliderColourIndex].z);
    }
}
