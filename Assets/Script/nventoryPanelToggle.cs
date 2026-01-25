using UnityEngine;

public class nventoryPanelToggle : MonoBehaviour
{
    public CanvasGroup panelGroup;
    private bool isVisible = false;

    void Start()
    {
        if (panelGroup == null) panelGroup = GetComponent<CanvasGroup>();
        // Скрыто
        if (panelGroup != null) panelGroup.alpha = 0;
        isVisible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isVisible = !isVisible;
            panelGroup.alpha = isVisible ? 1 : 0;
            Debug.Log("Toggled to: " + isVisible);
        }
    }
}