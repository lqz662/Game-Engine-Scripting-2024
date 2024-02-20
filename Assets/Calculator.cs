using UnityEngine;
using TMPro;

public class Calculator : MonoBehaviour
{
    public TextMeshProUGUI displayText; // Assign this in the Unity inspector

    private float prevInput = 0;
    private bool clearPrevInput = false;
    private EquationType equationType;

    private void Start()
    {
        Clear();
    }

    public void AddInput(string input)
    {
        if (clearPrevInput)
        {
            displayText.text = string.Empty;
            clearPrevInput = false;
        }

        displayText.text += input;
    }

    public void SetEquationType(EquationType type)
    {
        if (displayText.text == string.Empty) return;

        prevInput = float.Parse(displayText.text);
        clearPrevInput = true;
        equationType = type;
    }

    public void Calculate()
    {
        if (clearPrevInput || displayText.text == string.Empty) return;

        float currentInput = float.Parse(displayText.text);
        float result = 0;

        switch (equationType)
        {
            case EquationType.ADD:
                result = prevInput + currentInput;
                break;
            case EquationType.SUBTRACT:
                result = prevInput - currentInput;
                break;
            case EquationType.MULTIPLY:
                result = prevInput * currentInput;
                break;
            case EquationType.DIVIDE:
                if (currentInput != 0)
                    result = prevInput / currentInput;
                else
                {
                    displayText.text = "Error";
                    return;
                }
                break;
        }

        displayText.text = result.ToString();
        prevInput = result; // Store the result as the next 'prevInput'
        clearPrevInput = true;
        equationType = EquationType.None; // Reset equationType
    }

    public void Clear()
    {
        displayText.text = "0";
        clearPrevInput = false;
        prevInput = 0;
        equationType = EquationType.None;
    }

    // Utility function to call for each operation button
    public void OnAddClick() => SetEquationType(EquationType.ADD);
    public void OnSubtractClick() => SetEquationType(EquationType.SUBTRACT);
    public void OnMultiplyClick() => SetEquationType(EquationType.MULTIPLY);
    public void OnDivideClick() => SetEquationType(EquationType.DIVIDE);

    public enum EquationType
    {
        None = 0,
        ADD = 1,
        SUBTRACT = 2,
        MULTIPLY = 3,
        DIVIDE = 4
    }
}