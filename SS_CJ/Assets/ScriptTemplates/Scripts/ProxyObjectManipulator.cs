using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ProxyObjectManipulator : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject targetObject; // The object that should move when the controller object is interacted with

    private Vector3 initialControllerPosition;
    private Vector3 initialTargetPosition;
    private bool isDragging = false;

    void Start()
    {
        // Ensure the target object is assigned
       
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData) { }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        isDragging = true;
        initialControllerPosition = eventData.Pointer.Position;
        initialTargetPosition = targetObject.transform.position;
        Debug.Log("Pointer Down: Initial positions stored.");
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        if (isDragging)
        {
            // Calculate the offset of the controller's movement using pointer position
            Vector3 offset = eventData.Pointer.Position - initialControllerPosition;
            
            // Apply the offset to the target object's initial position
            targetObject.transform.position = initialTargetPosition + offset;
            Debug.Log("Pointer Dragged: Target Object Position: " + targetObject.transform.position);
        }
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        isDragging = false;
        Debug.Log("Pointer Up: Dragging stopped.");
    }
}
