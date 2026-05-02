using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections;

public class MagazineVR : MonoBehaviour
{
    private XRGrabInteractable grab;
    private Rigidbody rb;

    private VRGUNAK47 gun;
    private bool inserted = false;

    public bool canInsert = true;
    private bool wasRemoved = false;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        if (grab != null)
        {
            grab.selectEntered.AddListener(OnGrabbed);
            grab.selectExited.AddListener(OnReleased);
        }
    }

    void OnDestroy()
    {
        if (grab != null)
        {
            grab.selectEntered.RemoveListener(OnGrabbed);
            grab.selectExited.RemoveListener(OnReleased);
        }
    }

    public void InsertIntoGun(VRGUNAK47 ownerGun, Transform socket)
    {
        if (!canInsert) return;

        gun = ownerGun;
        inserted = true;
        wasRemoved = false;

        if (grab != null && grab.isSelected)
        {
            grab.interactionManager.SelectExit(
                (IXRSelectInteractor)grab.firstInteractorSelecting,
                grab
            );
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        rb.useGravity = false;

        transform.SetParent(socket);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    void OnGrabbed(SelectEnterEventArgs args)
    {
        if (!inserted) return;

        inserted = false;
        wasRemoved = true;
        canInsert = false;

        // Detach from gun
        transform.SetParent(null);

        rb.isKinematic = false;
        rb.useGravity = false;

        if (gun != null)
        {
            gun.RemoveMagazine();
            gun = null;
        }

        StartCoroutine(ReEnableInsert());
    }

    void OnReleased(SelectExitEventArgs args)
    {
        if (wasRemoved)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ReEnableInsert()
    {
        yield return new WaitForSeconds(0.5f);
        canInsert = true;
    }
}