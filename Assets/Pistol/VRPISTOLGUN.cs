using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRPistolGun : MonoBehaviour
{
    [Header("Gun Setup")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 70f;
    public float fireRate = 0.35f;   // Slower than AK, semi-auto

    [Header("Bullet Rotation Fix")]
    public Vector3 bulletRotationOffset = new Vector3(0f, 90f, 0f);

    [Header("Grab Setup")]
    public XRGrabInteractable grabInteractable;
    public Transform grabPoint;

    private float nextFireTime = 0f;

    void Start()
    {
        if (grabInteractable == null)
            grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabPoint != null)
            grabInteractable.attachTransform = grabPoint;

        // One trigger press = one shot
        grabInteractable.activated.AddListener(OnShoot);
    }

    void OnDestroy()
    {
        if (grabInteractable != null)
            grabInteractable.activated.RemoveListener(OnShoot);
    }

    void OnShoot(ActivateEventArgs args)
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Quaternion fixedRotation =
            firePoint.rotation *
            Quaternion.Euler(bulletRotationOffset);

        GameObject bullet =
            Instantiate(bulletPrefab, firePoint.position, fixedRotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }
    }
}