using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class TwutSubmitter : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Start()
    {
        //inputField.onSelect.AddListener((x) => Submit());
    }

    int i;
    public void Submit()
    {
        Debug.Log("Test! " + i++);
        Twutter.e.TwutFromMe(inputField.text);
        inputField.text = "";
    }
}
