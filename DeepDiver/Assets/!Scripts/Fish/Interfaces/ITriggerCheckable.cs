using UnityEngine;

public interface ITriggerCheckable
{
    bool IsScared { get; set; }
    bool IsGrabbed { get; set; }

    void SetScaredStatus(bool isScared);
    void SetGrabbedStatus(bool isGrabbed);
}