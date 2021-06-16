using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RemoteCameraMan : MonoBehaviour
{
    private void OnDisable()
    {
        data = null;
        Reset();
    }

    public Transform tripod;
    public Camera myCam;
    public void Reset()
    {
        myCam.transform.localPosition = Vector3.zero;
        myCam.transform.localRotation = Quaternion.identity;
        
        tripod.localPosition = Vector3.zero;
        tripod.localRotation = Quaternion.identity;
    }

    public RemoteGateInitData data;
    public void Init(RemoteGateInitData initData)
    {
        data = initData;
        data.SetupRefTransform(transform.parent, transform);
        
        myCam.transform.localPosition = data.camPos;
        myCam.transform.localRotation = data.camRot;
    }

    [Header("Debug Purpose")]
    [SerializeField]
    private Vector3 tripodTargetPos;
    [SerializeField]
    private Quaternion tripodTargetRot;
    private void FixedUpdate()
    {
        if (data != null && data.ShouldUpdate())
        {
            if (data.GetRefPosition(out tripodTargetPos))
            {
                tripod.localPosition = tripodTargetPos;
            }
            
            if (data.GetRefRotation(out tripodTargetRot))
            {
                tripod.localRotation = tripodTargetRot;
            }
        }
    }
}
