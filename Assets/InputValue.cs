using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputValue : MonoBehaviour
{
    [SerializeField] Slider sliderInput;
    [SerializeField] TMP_Text amountInput;
    [SerializeField] TMP_Text totalAmountInput;
    [SerializeField] TMP_Text totalPrice;
    [SerializeField] Gems gemsController;

    public string sliderInputString;
    public int sliderInputValue;
    // Start is called before the first frame update

    public int getWantedAmountOfGems() { return sliderInputValue; }

    // Update is called once per frame
    void Update()
    {
        sliderInputValue = (int)sliderInput.value;
        sliderInputString = sliderInput.value.ToString();
        amountInput.text = sliderInputString;
        totalAmountInput.text = sliderInputString + " K";
        totalPrice.text = (sliderInputValue * gemsController.getGemPrice()).ToString() + " E";
    }
}
