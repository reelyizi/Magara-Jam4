using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{   


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("A");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("B");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("C");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("D");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("E");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("F");
        }
    }
}
