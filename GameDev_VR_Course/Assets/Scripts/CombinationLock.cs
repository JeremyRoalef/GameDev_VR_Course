using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CombinationLock : MonoBehaviour
{
    [SerializeField]
    string numberCombination = "0412";

    [SerializeField]
    ButtonInteractable[] comboButtons = new ButtonInteractable[4];

    [SerializeField]
    TMP_Text textInput;

    const string defaultInputText = "0000";
    string userInput = "";

    private void Start()
    {
        for (int i = 0; i < comboButtons.Length; i++)
        {
            comboButtons[i].selectEntered.AddListener(OnComboButtonPressed);
        }

        textInput.text = defaultInputText;
    }

    private void OnComboButtonPressed(SelectEnterEventArgs arg0)
    {
        //Use SelectEnterEventArgs
        for (int i = 0; i < comboButtons.Length; i++)
        {
            //Find the object's name in the array to retrieve the index/number for the object
            if (arg0.interactableObject.transform.name == comboButtons[i].transform.name)
            {
                userInput += i.ToString();
                textInput.text = userInput;
            }

            //Reset the color of other objects to normal color when button is pressed
            comboButtons[i].SetColorToNormal();
        }
    }
}
