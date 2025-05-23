using UnityEngine;
using TMPro;

public class ClickHandler : MonoBehaviour
{
    public GameObject Helmet;
    public GameObject MenuHologram;
    public Camera camaraPrincipal;
    public TextMeshProUGUI textoTemporizado;
    public BoxCollider buttonCollider;

    private bool visorHasBeenActivated = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!visorHasBeenActivated)
            {
                visorHasBeenActivated = true;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camaraPrincipal.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (Helmet != null)
                        Helmet.SetActive(true);

                    if (buttonCollider != null)
                        buttonCollider.enabled = false;

                    if (MenuHologram != null)
                        MenuHologram.SetActive(false);

                    CameraController controlador = camaraPrincipal.GetComponent<CameraController>();
                    if (controlador != null)
                        controlador.enabled = true;

                    Debug.Log("¡Click exitoso!");

                    StartCoroutine(ActivarTextoConRetraso(15f));
                }
            }
        }
    }

    private System.Collections.IEnumerator ActivarTextoConRetraso(float segundos)
    {
        yield return new WaitForSeconds(segundos);

        if (!visorHasBeenActivated)
        {
            if (textoTemporizado != null)
                textoTemporizado.gameObject.SetActive(true);
        }
    }
}
