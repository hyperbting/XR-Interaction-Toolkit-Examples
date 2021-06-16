using System.Collections;
using UnityEngine;

public interface IReferenceObject
{
    GameObject GetGameObject();
    Vector3 GetCurrentPosition();
    Quaternion GetCurrentRotation();
}
