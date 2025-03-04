using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircularMenu : MonoBehaviour
{
    public Transform[] buttons; // Assign button transforms in order
    public Transform centerPosition;
    public float rotationDuration = 0.5f; // Smooth transition time

    private Dictionary<Transform, Vector3> positions = new Dictionary<Transform, Vector3>();

    void Start()
    {
        // Store initial positions
        foreach (Transform button in buttons)
        {
            positions[button] = button.position;
        }
    }

    public void SelectButton(Transform selected)
    {
        StartCoroutine(RotateButtons(selected));
    }

    IEnumerator RotateButtons(Transform selected)
    {
        Vector3 selectedStartPos = selected.position;
        Dictionary<Transform, Vector3> newPositions = new Dictionary<Transform, Vector3>();

        // Determine new positions
        foreach (var button in buttons)
        {
            if (button == selected)
                continue;
            
            // Find the button that should take this button’s old position
            Transform nextButton = GetNextButton(button);
            newPositions[nextButton] = positions[button];
        }

        newPositions[selected] = centerPosition.position;

        // Animate movement
        float elapsed = 0;
        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;
            foreach (var button in buttons)
            {
                button.position = Vector3.Lerp(button.position, newPositions[button], elapsed / rotationDuration);
            }
            yield return null;
        }

        // Update positions
        foreach (var button in buttons)
        {
            positions[button] = button.position;
        }
    }

    Transform GetNextButton(Transform current)
    {
        int index = System.Array.IndexOf(buttons, current);
        int nextIndex = (index + 1) % buttons.Length;
        return buttons[nextIndex];
    }
}
