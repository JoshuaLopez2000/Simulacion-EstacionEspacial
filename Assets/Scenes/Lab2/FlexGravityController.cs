using UnityEngine;
using NVIDIA.Flex;

public class FlexGravityHack : MonoBehaviour
{
    public FlexContainer flexContainerObject;

    void Start()
    {
        var container = flexContainerObject;
        if (container == null)
        {
            Debug.LogError("No FlexContainer encontrado.");
            return;
        }

        // Intenta usar Reflection para buscar campos internos
        var field = typeof(FlexContainer).GetField("m_simpleGravity", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field != null)
        {
            //field.SetValue(container, new Vector3(0, -30f, 0));
            Debug.Log("Gravedad cambiada por reflexión.");
        }
        else
        {
            Debug.LogWarning("No se encontró el campo interno 'm_simpleGravity'.");
        }

    }

    void Update()
    {
        // Alternar gravedad entre 0 y -9.81f al presionar la tecla G
        if (Input.GetKeyDown(KeyCode.G))
        {
            var container = flexContainerObject;
            if (container == null)
            {
                Debug.LogError("No FlexContainer encontrado.");
                return;
            }

            // Intenta usar Reflection para buscar campos internos
            var field = typeof(FlexContainer).GetField("m_simpleGravity", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                Vector3 currentGravity = (Vector3)field.GetValue(container);
                Vector3 newGravity = currentGravity.y == 0 ? new Vector3(0, -9.81f, 0) : Vector3.zero;
                field.SetValue(container, newGravity);
                Debug.Log($"Gravedad cambiada a: {newGravity}");
                container.AddActor(actor: null); // Forzar actualización del contenedor
            }
            else
            {
                Debug.LogWarning("No se encontró el campo interno 'm_simpleGravity'.");
            }
        }
    }
}
