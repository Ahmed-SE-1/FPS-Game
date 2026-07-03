using UnityEngine;
using UnityEngine.UI; // Using standard UI

public class AmmoBoxScript : MonoBehaviour
{
    public ShootingScript playerShootingScript;
    public Text promptText; // Changed to Legacy Text
    private bool isNear = false;

    void Update()
    {
        if (isNear)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerShootingScript.RefillAmmo();
                if (promptText != null)
                {
                    promptText.gameObject.SetActive(false);
                }
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            promptText.text = "Press R to Fill the Ammo";
            promptText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            promptText.gameObject.SetActive(false);
        }
    }
}