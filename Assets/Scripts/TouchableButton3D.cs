using UnityEngine;
using TMPro;

public class TouchableButton3D : MonoBehaviour
{
    public Material selectedMaterial;
    public Renderer targetRenderer;
    public TextMeshProUGUI counterText;
    public GameObject VictoryPanel;
    public FreeCameraController cameraController;
    private static int totalSelected = 0;
    private static TextMeshProUGUI sharedCounterText;
    private static GameObject sharedPanelToActivate;
    private static FreeCameraController sharedCameraController;
    private bool isSelected = false;

    void Start()
    {
        totalSelected = 0;
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        if (sharedCounterText == null && counterText != null)
        {
            sharedCounterText = counterText;
            UpdateCounter();
        }

        if (sharedPanelToActivate == null && VictoryPanel != null)
            sharedPanelToActivate = VictoryPanel;

        if (sharedCameraController == null && cameraController != null)
            sharedCameraController = cameraController;
    }

    void OnMouseDown()
    {
        if (isSelected) return;

        if (targetRenderer != null && selectedMaterial != null)
        {
            Material[] newMaterials = new Material[targetRenderer.materials.Length];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = selectedMaterial;
            }

            targetRenderer.materials = newMaterials;
        }

        isSelected = true;
        totalSelected++;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        if (sharedCounterText != null)
            sharedCounterText.text = $"{totalSelected} de 5";

        bool victory = totalSelected >= 5;

        if (sharedCameraController != null && victory)
            sharedCameraController.DisableCamera();

        if (sharedPanelToActivate != null)
            sharedPanelToActivate.SetActive(victory);

    }
}
