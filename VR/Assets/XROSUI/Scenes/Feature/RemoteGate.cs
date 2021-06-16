using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class RemoteGate : MonoBehaviour
{
    public RemoteCameraMan camMan;

    public bool Init(RemoteGateInitData initData)
    {
        // Check/Enable camera
        camMan.Init(initData);//SetupCamera(initData.camPos, initData.camRot);
        
        //
        
        return true;
    }
    
    public void Activate()
    {
    }
    
    public void Deactivate()
    {
        // Check/Disable camera
        
        // Reset Camera
        camMan.Reset();
    }
}

