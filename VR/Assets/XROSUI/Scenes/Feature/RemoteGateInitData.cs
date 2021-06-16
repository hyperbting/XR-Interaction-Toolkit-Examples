using System;
using UnityEngine;
using UnityEngine.Animations;

[Serializable]
public class RemoteGateInitData
{
    public IReferenceObject refObject;
    public Vector3 camPos = Vector3.zero;
    public Quaternion camRot = Quaternion.identity;

    [Header("Debug Purpose")]
    
    // #region follow rule
    // public bool positionX = true;
    // public bool positionY = true;
    // public bool positionZ = true;

    // public bool RotationX = true;
    // public bool RotationY = true;
    // public bool RotationZ = true;
    // #endregion

    [SerializeField]
    private Vector3 tripodBaseRefPos;
    [SerializeField]
    private Quaternion tripodBaseRefRot;

    public bool ShouldUpdate()
    {
        if (refObject == null)
            return false;

        return true;
    }

    public void SetupRefTransform(Transform parentTran, Transform tran)
    {
        tripodBaseRefPos = refObject.GetCurrentPosition();
        tripodBaseRefRot = refObject.GetCurrentRotation();

        parentTran.localPosition = -tripodBaseRefPos;
        //parentTran.localRotation = -tripodBaseRefRot;
        
        tran.localPosition = tripodBaseRefPos;
        //tran.localRotation = tripodBaseRefRot;
    }

    public bool GetRefPosition(out Vector3 newPos)
    {
        newPos = Vector3.zero;
        
        if (refObject == null)
            return false;
        
        newPos = refObject.GetCurrentPosition();
        
        //TODO: if positionX/ positionY/ positionZ

        return true;
    }

    public bool GetRefRotation(out Quaternion newRot)
    {
        newRot = Quaternion.identity;
        
        if (refObject == null)
            return false;
        
        newRot = refObject.GetCurrentRotation();
            
        //TODO: if RotationX/ RotationY/ RotationZ

        return true;
    }
}