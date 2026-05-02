using UnityEngine;

public class AKMagInsertZone : MonoBehaviour
{
    public VRGUNAK47 gun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magazine"))
        {
            MagazineVR mag = other.GetComponent<MagazineVR>();

            if (mag != null && mag.canInsert)
            {
                gun.InsertMagazine(mag);
            }
        }
    }
}