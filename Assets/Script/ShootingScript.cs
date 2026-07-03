using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingScript : MonoBehaviour
{
    public float targetDistance = 0;
    public int allowedDistance = 8;
    public int damageAmount = 5;

    [Header("Ammo Settings")]
    public int maxAmmo = 8;
    public int currentAmmo;
    public Text ammoText;

    [Header("Crosshair Animators")]
    public Animator topCursAnim;
    public Animator bottomCursAnim;
    public Animator leftCursAnim;
    public Animator rightCursAnim;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI();

        // Trigger all four crosshair animations at once
        if (topCursAnim != null) topCursAnim.SetTrigger("Shoot");
        if (bottomCursAnim != null) bottomCursAnim.SetTrigger("Shoot");
        if (leftCursAnim != null) leftCursAnim.SetTrigger("Shoot");
        if (rightCursAnim != null) rightCursAnim.SetTrigger("Shoot");

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            targetDistance = hit.distance;
            if (targetDistance < allowedDistance)
            {
                hit.transform.SendMessage("DamageEnemy", damageAmount, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void RefillAmmo()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo + " / " + maxAmmo;
        }
    }
}