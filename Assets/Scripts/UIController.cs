using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Serializable]
    public class ButtonPanelPair
    {
        public Button button;
        public GameObject panel;
    }

    public List<ButtonPanelPair> buttonPanelPairs;
    private GameObject activePanel;
    
    void Start()
    {
        foreach (var pair in buttonPanelPairs)
        {
            pair.button.onClick.AddListener(() => ShowPanel(pair.panel));
        }
    }

    void ShowPanel(GameObject panel)
    {
        activePanel ?.SetActive(false);
        
    }
}
