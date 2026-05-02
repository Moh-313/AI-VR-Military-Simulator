using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRGUNAK47 : MonoBehaviour
{
    [Header("Gun Setup")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 80f;
    public float fireRate = 0.1f;

    [Header("Ammo")]
    public int maxAmmo = 30;
    private int currentAmmo = 0;

    [Header("Magazine")]
    public Transform magSocket;
    public MagazineVR currentMagazine;

    [Header("Grab")]
    public XRGrabInteractable grabInteractable;
    public Transform grabPoint;

    [Header("Bullet Rotation")]
    public Vector3 bulletRotationOffset = new Vector3(0f, -90f, 0f);

    private bool triggerHeld = false;
    private float nextFireTime = 0f;

    void Start()
    {
        if (grabInteractable == null)
            grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabPoint != null && grabInteractable != null)
            grabInteractable.attachTransform = grabPoint;

        if (grabInteractable != null)
        {
            grabInteractable.activated.AddListener(OnTriggerPressed);
            grabInteractable.deactivated.AddListener(OnTriggerReleased);
        }
    }

    void Update()
    {
        if (triggerHeld && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.activated.RemoveListener(OnTriggerPressed);
            grabInteractable.deactivated.RemoveListener(OnTriggerReleased);
        }
    }

    void OnTriggerPressed(ActivateEventArgs args)
    {
        triggerHeld = true;
    }

    void OnTriggerReleased(DeactivateEventArgs args)
    {
        triggerHeld = false;
    }

    void Shoot()
    {
        if (currentMagazine == null || currentAmmo <= 0)
        {
            Debug.Log("Empty / No magazine");
            return;
        }

        currentAmmo--;

        Vector3 shootDirection = -firePoint.right;

        Quaternion bulletRotation =
            Quaternion.LookRotation(shootDirection) *
            Quaternion.Euler(bulletRotationOffset);

        GameObject bullet =
            Instantiate(bulletPrefab, firePoint.position, bulletRotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
            rb.linearVelocity = shootDirection * bulletSpeed;

        Debug.Log("Ammo Left: " + currentAmmo);
    }

    public void InsertMagazine(MagazineVR mag)
    {
        if (currentMagazine != null)
            return;

        currentMagazine = mag;
        currentAmmo = maxAmmo;

        mag.InsertIntoGun(this, magSocket);
    }

    public void RemoveMagazine()
    {
        currentMagazine = null;
        currentAmmo = 0;
    }
}