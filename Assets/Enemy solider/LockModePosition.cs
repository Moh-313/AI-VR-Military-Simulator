using UnityEngine;

public class LockModePosition : MonoBehaviour
{
    void LateUpdate()
    {
        transform.localPosition = Vector3.zero;
    }
}