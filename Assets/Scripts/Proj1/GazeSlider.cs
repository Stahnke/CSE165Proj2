using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeSlider : MonoBehaviour {

    public Slider slider;

    public void UpdateSlider(float newValue)
    {
        slider.value = newValue;
    }
}
