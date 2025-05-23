using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EnergyDisplay : MonoBehaviour
{
    public Animator animator;
    public float maxEnergy = 180f;         // 100% de energía
    public float fastMultiplier = 5f;      // Velocidad de consumo acelerado
    public GameObject interfaceToActivate; // UI que se activa cuando se acaba la energía
    public FreeCameraController FreeCameraController;

    private float currentEnergy;
    private TextMeshProUGUI energyText;
    private bool interfaceActivated = false;

    void Start()
    {
        currentEnergy = maxEnergy;
        energyText = GetComponent<TextMeshProUGUI>();

        if (interfaceToActivate != null)
            interfaceToActivate.SetActive(false); // Asegura que esté desactivada al inicio
    }

    void Update()
    {
        if (currentEnergy <= 0f)
        {
            currentEnergy = 0f;

            if (!interfaceActivated)
            {

                if (FreeCameraController != null)
                {
                    var cameraScript = FreeCameraController as FreeCameraController;
                    if (cameraScript != null)
                        cameraScript.DisableCamera();
                    else
                        FreeCameraController.enabled = false;
                }

                if (interfaceToActivate != null)
                    interfaceToActivate.SetActive(true);




                interfaceActivated = true;
            }
        }
        else
        {
            bool hasGlass = animator != null && animator.GetBool("HasGlass");
            float delta = Time.deltaTime * (hasGlass ? fastMultiplier : 1f);
            currentEnergy -= delta;
        }

        float energyPercent = Mathf.Clamp01(currentEnergy / maxEnergy) * 100f;
        energyText.text = $"{energyPercent:0}";
    }
}
