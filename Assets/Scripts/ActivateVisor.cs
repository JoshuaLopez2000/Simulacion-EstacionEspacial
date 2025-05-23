using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivateVisor : MonoBehaviour
{
    bool visorActive = false;
    public Animator visorAnimation;
    public TextMeshProUGUI hintText;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hintText != null)
                hintText.gameObject.SetActive(false);
            visorActive = !visorActive;
            visorAnimation.SetBool("HasGlass", visorActive);
            Debug.Log("Visor Active: " + visorActive);
        }
    }
}
