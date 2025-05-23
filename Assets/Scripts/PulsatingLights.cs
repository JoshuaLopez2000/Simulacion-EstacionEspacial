using UnityEngine;

public class PulsatingLights : MonoBehaviour
{
    public float minIntensity = 0.5f;    // Intensidad mínima de la luz
    public float maxIntensity = 2.0f;    // Intensidad máxima de la luz
    public float speed = 2.0f;           // Velocidad de pulso

    private Light[] alertLights;

    void Start()
    {
        // Buscar todas las luces en los hijos
        alertLights = GetComponentsInChildren<Light>();
    }

    void Update()
    {
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(Time.time * speed) + 1f) / 2f);

        foreach (Light light in alertLights)
        {
            light.intensity = intensity;
        }
    }
}
