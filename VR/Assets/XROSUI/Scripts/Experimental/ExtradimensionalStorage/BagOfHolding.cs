using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagOfHolding : MonoBehaviour, IReferenceObject
{
    public GameObject peekWindow;
    
    #region Properties
    [Header("Debug Purpose")]
    [SerializeField] private GameObject remoteGate;
    public GameObject RemoteGateGO
    {
        get
        {
            if(remoteGate == null)
                Debug.LogError("Interacting with Null RemoteGate");
            
            return remoteGate;
        }
        set
        {
            remoteGate = value;
        }
    }

    public CounterHelper detachCountdown = new CounterHelper("DetachCountdown", 3000);
    #endregion

    #region IReferenceObject
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }

    public Quaternion GetCurrentRotation()
    {
        return transform.rotation;
    }
    #endregion
    
    private void OnDestroy()
    {
        Deactivate();
    }

    private void OnEnable()
    {
        Setup();
    }

    void FixedUpdate()
    {
        if (detachCountdown.IsReachLimit(false))
        {
            //Do Detach
        }
        else
        {
            detachCountdown.Increment();
        }
    }

    // or interface
    private RemoteGate rGate;
    public void Setup(GameObject remoteGateGO =null)
    {
        if (remoteGateGO != null)
            RemoteGateGO = remoteGateGO;
            
        rGate =  RemoteGateGO?.GetComponent<RemoteGate>();
        if (rGate == null)
            return;

        var initData = new RemoteGateInitData()
        {
            refObject = this
        };
        rGate.Init(initData);
    }
    
    /// <summary>
    /// Called when Grabbed?
    /// </summary>
    public void Activate()
    {
        //Begin AutoDetach
        detachCountdown.Reset();
        detachCountdown.Run(true);
        
        if (rGate == null)
            return;
        
        rGate.Activate();
        
        peekWindow.SetActive(true);
    }

    /// <summary>
    /// Called when Released
    /// </summary>
    public void Deactivate()
    {
        detachCountdown.Run(false);
        
        //hide content
        peekWindow.SetActive(false);
    }
}
