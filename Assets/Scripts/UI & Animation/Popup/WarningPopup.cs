using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class WarningPopup : EscapeableUI
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;

    private bool use1Btn = false;
    private bool isEnableEscape;

    public void DestroyPopup()
    {
        Destroy(gameObject);
    }
    public void Set2BtnPopup(string title, string description, string okText, string cancelText, 
        UnityAction okListener, UnityAction cancelListener, bool isEnableEscape = true)
    {
        titleText.text = title;
        descriptionText.text = description;
        okButton.GetComponentInChildren<TMP_Text>().text = okText;
        cancelButton.GetComponentInChildren<TMP_Text>().text = cancelText;
        okButton.onClick.AddListener(okListener);
        cancelButton.onClick.AddListener(cancelListener);
        this.isEnableEscape = isEnableEscape;
    }
    public void Set2BtnPopup(string title, string description, 
        UnityAction okListener, UnityAction cancelListener, bool isEnableEscape = true)
    {
        titleText.text = title;
        descriptionText.text = description;
        okButton.onClick.AddListener(okListener);
        cancelButton.onClick.AddListener(cancelListener);
        use1Btn = false;
        this.isEnableEscape = isEnableEscape;
    }
    public void Set1BtnPopup(string title, string description, string okText,
        UnityAction okListener, bool isEnableEscape = true)
    {
        titleText.text = title;
        descriptionText.text = description;
        okButton.GetComponentInChildren<TMP_Text>().text = okText;
        okButton.onClick.AddListener(okListener);
    }
    public void Set1BtnPopup(string title, string description,
        UnityAction okListener, bool isEnableEscape = true)
    {
        titleText.text = title;
        descriptionText.text = description;
        okButton.onClick.AddListener(okListener);
    }
}
