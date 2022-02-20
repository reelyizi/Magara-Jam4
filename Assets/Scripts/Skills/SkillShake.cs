using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShake : MonoBehaviour
{
    void Awake()
    {
        Camera.main.gameObject.GetComponent<CameraManager>().screenShake = true;
    }
}
