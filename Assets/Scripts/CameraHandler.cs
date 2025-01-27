using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public GameObject catClone;
    public CinemachineTargetGroup targetGroup;

    void Update()
    {
        catClone = GameObject.Find("Cat Assassin(Clone)");
        targetGroup.AddMember(catClone.transform, 1f, 0f);
        
    }
}
