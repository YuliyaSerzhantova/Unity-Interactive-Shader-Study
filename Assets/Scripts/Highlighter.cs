using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class Highlighter : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    [SerializeField]
    private Material originalMaterial;
    [SerializeField]
    private Material highlightedMaterial;

    // Stores the current highlight state
    private bool isHighlighted = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // Initialize with original material
        EnableHighlight(false);
    }

    // Method to switch materials based on the highlight state
    public void EnableHighlight(bool onOff)
    {
        if (meshRenderer != null && originalMaterial != null && highlightedMaterial != null)
        {
            meshRenderer.material = onOff ? highlightedMaterial : originalMaterial;
            isHighlighted = onOff;
        }
    }

    void Update()
    {
        // 1. Create a ray from the current mouse position
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        // 2. Perform physics raycasting
        if (Physics.Raycast(ray, out hit))
        {
            // 3. Check if the ray hit this specific object
            if (hit.transform == this.transform)
            {
                if (!isHighlighted)
                {
                    EnableHighlight(true);
                }
            }
            else
            {
                // If hitting another object while this one is still highlighted
                if (isHighlighted)
                {
                    EnableHighlight(false);
                }
            }
        }
        else
        {
            // 4. If the ray hits nothing at all
            if (isHighlighted)
            {
                EnableHighlight(false);
            }
        }
    }
}
