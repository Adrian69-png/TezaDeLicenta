using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private GameObject usernameInputPanel;
    void Start()
    {
        if (PlayerPrefs.HasKey("Username"))
        {
            usernameText.text = PlayerPrefs.GetString("Username"); 
            usernameInputPanel.SetActive(false);
        }
        else
        {
            usernameInputPanel.SetActive(true);
        }
    }

    public void ChangeUserName()
    {
        if (!string.IsNullOrEmpty(usernameText.text))
        {
            usernameText.text = usernameInputField.text;
            PlayerPrefs.SetString("Username", usernameText.text);
            usernameInputPanel.SetActive(false);
        }
    }
}
