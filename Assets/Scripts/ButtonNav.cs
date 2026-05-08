using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ButtonNav : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private Selectable elementToSelect;

    [SerializeField] private bool showVisualization;
    private Color navigationColour = Color.white;

    private void OnDrawGizmos()
    {
        if (!showVisualization)
            return;
        if (elementToSelect == null)
            return;

        Gizmos.color = navigationColour;
        Gizmos.DrawLine(gameObject.transform.position, elementToSelect.transform.position);
    }

    private void Reset()
    {
        eventSystem = FindFirstObjectByType<EventSystem>();

        if (eventSystem == null)
            Debug.Log("There is no event system in your scene :(");
    }

    public void JumpToElement()
    {
        if (elementToSelect == null)
            Debug.Log("ELEMENT NULL");

        eventSystem.SetSelectedGameObject(elementToSelect.gameObject);
    }

    public void JumpToSpecificElement(string element)
    {
        GameObject elementToFind = GameObject.FindWithTag(element);

        eventSystem.SetSelectedGameObject(elementToFind);

        Debug.Log("FINDING " + elementToFind);
    }

}

