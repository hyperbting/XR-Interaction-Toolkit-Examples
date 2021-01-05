using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class VE_Avatar : VrEquipment
{
    public bool showDebugUI;

    Dictionary<System.Guid, (GameObject, Vector3)> detectedIHeadDetectable = new Dictionary<System.Guid, (GameObject, Vector3)>();
    [SerializeField] GameObject debugUIGO;

    public void UnRegister(IHeadDetectable itf)
    {
        itf.Clean();
        detectedIHeadDetectable.Remove(itf.GetUUID);
    }

    public Vector3? TryGetPositionDelta(IHeadDetectable ihd)
    {
        if (detectedIHeadDetectable.TryGetValue(ihd.GetUUID, out (GameObject, Vector3) val))
        {
            return val.Item2;
        }

        return null;
    }

    #region mono
    private void OnEnable()
    {
        debugUIGO?.SetActive(showDebugUI);
    }

    private void OnTriggerEnter(Collider other)
    {
        var itf = other.gameObject.GetComponent<IHeadDetectable>();
        if (itf == null)
            return;

        itf.Assign(this);
        detectedIHeadDetectable[itf.GetUUID] = (other.gameObject, Vector3.zero);
    }

    private void OnTriggerExit(Collider other)
    {
        var itf = other.gameObject.GetComponent<IHeadDetectable>();
        if (itf == null)
            return;

        UnRegister(itf);
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        base.Update();

        var keyList = new List<System.Guid>(detectedIHeadDetectable.Keys);
        foreach (var key in keyList)
        {
            var val = detectedIHeadDetectable[key];
            var deltaPos = val.Item1.transform.position - transform.position;
            detectedIHeadDetectable[key] = (val.Item1, deltaPos);
        }
    }
    #endregion

    #region Create Avatar Doll
    //only one is allowed
    [SerializeField] bool usingAvatarDoll = false;
    public void AttachAvatarDoll(Transform parent)
    {
        if (usingAvatarDoll)
        {
            Debug.LogWarning($"Only One AvatarDoll at one time");
            return;
        }

        usingAvatarDoll = true;

        // How to Create/ Show Avator Doll?

    }

    public void UnuseAvatarDoll()
    {
        if (!usingAvatarDoll)
        {
            Debug.LogWarning($"AvatarDoll NotInUse");
            return;
        }

        usingAvatarDoll = false;

        // How to Hide/ Destroy Avator Doll?

    }
    #endregion

    protected override void OnHoverEnter(XRBaseInteractor obj)
    {
        Core.Ins.Avatar.ShowAvatarManagementMode(true);
    }

    protected override void OnLastHoverExit(XRBaseInteractor obj)
    {
        Core.Ins.Avatar.ShowAvatarManagementMode(false);
    }
    
    protected override void OnActivate(XRBaseInteractor obj)
    {
        //Toggle On/Off Avatar Management Mode
        Core.Ins.Avatar.ToggleAvatarManagementModeLock();
    }

    public override void HandleGesture(ENUM_XROS_Gesture gesture, float distance)
    {
        switch (gesture)
        {
            case ENUM_XROS_Gesture.Up:
                break;
            case ENUM_XROS_Gesture.Down:
                break;
            case ENUM_XROS_Gesture.Forward:
                break;
            case ENUM_XROS_Gesture.Backward:
                break;
            case ENUM_XROS_Gesture.Left:
                Core.Ins.Avatar.PreviousAlternateAvatar();
                break;
            case ENUM_XROS_Gesture.Right:
                Core.Ins.Avatar.NextAlternateAvatar();
                break;
            case ENUM_XROS_Gesture.RotateClockwise:
                break;
            case ENUM_XROS_Gesture.RotateCounterclockwise:
                break;
            default:
                break;
        }
    }
}

public interface IHeadDetectable
{
    System.Guid GetUUID { get; }
    ControllerType ConType { get; }

    System.Action<bool, Vector3> OnRelativePositionChanged { get; set; }

    void Assign(VE_Avatar hd);
    void Clean();
}