using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableSceneChanger : MonoBehaviour
{
    // Nombre o índice de la escena a cargar
    public string sceneName;

    private void OnMouseDown()
    {
        // Cambia la escena cuando haces click sobre el collider
        SceneManager.LoadScene(sceneName);
    }
}
