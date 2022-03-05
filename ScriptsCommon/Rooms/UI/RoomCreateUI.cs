using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoomCreateUI : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]
    private Slider betSlider;
    [SerializeField]
    private Slider peopleSlider;
    [Space(5f)]
    [Header("Outputs")]
    [SerializeField]
    private TMP_Text betText;
    [SerializeField]
    private TMP_Text peopleText;

    public void DataUpdate()
    {
        betText.text =Convert.ToString( betSlider.value *100);
        peopleText.text = Convert.ToString(peopleSlider.value);
    }

}
