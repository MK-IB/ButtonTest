using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Animator skillsAnimator;
    [SerializeField] private Transform buttonsHolder;
    [SerializeField] private List<Transform> buttons;
    [SerializeField] private List<Transform> buttonPositions;
    
    public float rotationDuration = 0.5f;
    
    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;
    
    public class ButtonPanelPair
    {
        public Button button;
        public GameObject panel;
    }
    private GameObject activePanel;
    
    void Start()
    {
       
    }
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0)) // Right-click
        {
            if (IsPointerOverUIElement(skillsPanel))
            {
                ClosePanel();
                Debug.Log("Closing Skills Panel...");
            }
        }*/
    }
    bool IsPointerOverUIElement(GameObject panel)
    {
        pointerEventData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };
        
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);
        
        foreach (var result in results)
        {
            if (result.gameObject == panel)
                return true;
        }
        return false;
    }

    public void ShowPanel(Transform button)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons.Contains(button))
            {
                button.parent = buttonsHolder;
                StartCoroutine(MoveButton(button, i));
                
            }
        }

        skillsAnimator.SetBool("Open", true);
    }

    IEnumerator MoveButton(Transform btn, int index)
    {
        float elapsed = 0;
        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;
            btn.position = Vector3.Lerp(btn.position, buttonPositions[index].position, elapsed / rotationDuration);
            yield return null;
        }
    }

    public void ClosePanel()
    {
        skillsAnimator.SetBool("Open", false);
        skillsAnimator.SetBool("Close", true);
    }
}
