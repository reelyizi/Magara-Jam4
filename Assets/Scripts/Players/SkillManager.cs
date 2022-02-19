using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private List<SkillSlot> skillSlots = new List<SkillSlot>();
    [SerializeField] private Transform effectPosition;
    private float elapsedTime;

    private bool flag;
    [SerializeField] private Skill cloneSkill = null;
    private float duringSkillTimer;
    private int index = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !flag)
        {
            GetComponent<PlayerMovement>().enabled = false;
            //elapsedTime = skillSlots[0].skillObject.duration[0];
            cloneSkill = skillSlots[0].skillObject;
            flag = true;
            //Instantiate(skillSlots[0].skillObject.animation, effectPosition.position, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !flag)
        {
            Debug.Log("B");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !flag)
        {
            Debug.Log("C");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && !flag)
        {
            Debug.Log("D");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && !flag)
        {
            Debug.Log("E");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && !flag)
        {
            Debug.Log("F");
        }

        if (flag)
        {
            for (int i = index; i < cloneSkill.animation.Count && elapsedTime <= 0; i++)
            {
                Debug.Log(index);
                index = i + 1;

                if (i < cloneSkill.animation.Count - 1)
                    elapsedTime = cloneSkill.cooldown[i];
                else
                {
                    GetComponent<PlayerMovement>().enabled = true;
                    flag = false;
                    index = 0;
                }

                GameObject spawnedSkillEffect = Instantiate(cloneSkill.animation[i], effectPosition.position, effectPosition.rotation);
                Debug.Log(cloneSkill.animation[i].name);
                Destroy(spawnedSkillEffect, cloneSkill.spawnDuration[(i != cloneSkill.animation.Count - 1) ? i : i - 1]);
                //Destroy(spawnedSkillEffect, 2f);
            }

            elapsedTime -= Time.deltaTime;
        }
    }
}
