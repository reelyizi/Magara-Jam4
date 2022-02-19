using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> skillSlots = new List<GameObject>();
    [SerializeField] private Transform effectPosition;
    private float elapsedTime;

    private bool skillFlag;
    [SerializeField] private Skill cloneSkill = null;
    private int index = 0;

    void Update()
    {       
        if (Input.GetKeyDown(KeyCode.Alpha1) && !skillFlag && !SkillCooldownManagar._instance.bound.Contains(skillSlots[0]))
        {
            GenerateSkill(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !skillFlag && !SkillCooldownManagar._instance.bound.Contains(skillSlots[1]))
        {
            GenerateSkill(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !skillFlag)
        {
            Debug.Log("C");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && !skillFlag)
        {
            Debug.Log("D");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && !skillFlag)
        {
            Debug.Log("E");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && !skillFlag)
        {
            Debug.Log("F");
        }

        if (skillFlag)
        {
            for (int i = index; i < cloneSkill.animation.Count && elapsedTime <= 0; i++)
            {
                index = i + 1;

                if (i < cloneSkill.animation.Count - 1)
                    elapsedTime = cloneSkill.cooldown[i];
                else
                {
                    GetComponent<PlayerMovement>().enabled = true;
                    skillFlag = false;
                    index = 0;
                }

                GameObject spawnedSkillEffect = null;
                if (cloneSkill.positionType == SkillPositionType.frontCharacter)
                    spawnedSkillEffect = Instantiate(cloneSkill.animation[i], effectPosition.position, effectPosition.rotation);
                else if (cloneSkill.positionType == SkillPositionType.character)
                    spawnedSkillEffect = Instantiate(cloneSkill.animation[i], transform.position, effectPosition.rotation);

                //Destroy(spawnedSkillEffect, cloneSkill.spawnDuration[(i != cloneSkill.animation.Count - 1) ? i : i - 1]);
                Destroy(spawnedSkillEffect, cloneSkill.spawnDuration[i]);
                //Destroy(spawnedSkillEffect, 2f);
            }

            elapsedTime -= Time.deltaTime;
        }
    }

    private void GenerateSkill(int number)
    {
        GetComponent<PlayerMovement>().enabled = false;
        cloneSkill = skillSlots[number].GetComponent<SkillSlot>().skillObject;
        skillFlag = true;
        SkillCooldownManagar._instance.AddCooldown(skillSlots[number], cloneSkill.skillCooldown);
    }
}
