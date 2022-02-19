using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillManager : MonoBehaviour
{
    public static SkillManager _instance;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField] private GameObject VFX;
    [SerializeField] private List<GameObject> skillSlots = new List<GameObject>();

    [SerializeField] private List<GameObject> skillParticle = new List<GameObject>();
    [SerializeField] private List<float> skillDelay = new List<float>();
    [SerializeField] private List<SkillPositionType> skillPositionTypes = new List<SkillPositionType>();

    [SerializeField] private Transform effectPosition;
    [SerializeField] private Transform groundEffectPosition;
    private float elapsedTime;

    private bool skillFlag;
    //private Skill cloneSkill = null;
    private int index = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !skillFlag && !SkillCooldownManagar._instance.bound.Contains(skillSlots[0]))
        {
            GenerateSkill(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !skillFlag && !SkillCooldownManagar._instance.bound.Contains(skillSlots[1]))
        {
            VFX.transform.parent = null;
            transform.parent = VFX.transform;

            Animator characterAnimator = VFX.GetComponent<Animator>();
            characterAnimator.SetTrigger("Skill");
            characterAnimator.SetInteger("Skill Type", 1);
            characterAnimator.SetInteger("Skill Level", 1);
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

            /*
            for (int i = index; i < cloneSkill.animation.Count && elapsedTime <= 0; i++)
            {
                index = i + 1;

                if (i < cloneSkill.animation.Count - 1)
                    elapsedTime = cloneSkill.cooldown[i];

                GameObject spawnedSkillEffect = null;
                if (cloneSkill.positionType == SkillPositionType.frontCharacter)
                    spawnedSkillEffect = Instantiate(cloneSkill.animation[i], effectPosition.position, effectPosition.rotation);
                else if (cloneSkill.positionType == SkillPositionType.character)
                    spawnedSkillEffect = Instantiate(cloneSkill.animation[i], transform.position, effectPosition.rotation);

                //Destroy(spawnedSkillEffect, cloneSkill.spawnDuration[(i != cloneSkill.animation.Count - 1) ? i : i - 1]);
                Destroy(spawnedSkillEffect, cloneSkill.spawnDuration[i]);
                //Destroy(spawnedSkillEffect, 2f);
            }
            */

            if (skillDelay.Any())
            {
                for (int i = 0; i < skillDelay.Count; i++)
                {
                    Debug.Log("A");
                    skillDelay[i] -= Time.deltaTime;
                    if (skillDelay[i] <= 0)
                    {
                        GenerateEffect(index);

                        skillDelay.RemoveAt(i);
                        skillParticle.RemoveAt(i);
                        skillPositionTypes.RemoveAt(i);
                    }
                }
            }

            //elapsedTime -= Time.deltaTime;
        }
    }

    private void GenerateSkill(int number)
    {
        GetComponent<PlayerMovement>().enabled = false;
        //cloneSkill = skillSlots[number].GetComponent<SkillSlot>().skillObject;
        skillFlag = true;
        SkillCooldownManagar._instance.AddCooldown(skillSlots[number], skillSlots[number].GetComponent<SkillSlot>().skillObject.skillCooldown);
    }

    public void AddEffectList(List<float> delay, List<GameObject> particle, List<SkillPositionType> spt)
    {
        delay.ForEach(delegate (float delayTime) { skillDelay.Add(delayTime * 3); });
        particle.ForEach(delegate (GameObject particleObject) { skillParticle.Add(particleObject); });
        spt.ForEach(delegate (SkillPositionType spType) { skillPositionTypes.Add(spType); });
    }

    private void GenerateEffect(int index)
    {
        GameObject effect = null;
        if (skillPositionTypes[index] == SkillPositionType.character)
            effect = Instantiate(skillParticle[index], transform.position, transform.rotation);
        else if (skillPositionTypes[index] == SkillPositionType.frontCharacter)
            effect = Instantiate(skillParticle[index], effectPosition.position, effectPosition.rotation);
        else if (skillPositionTypes[index] == SkillPositionType.groundObject)
            effect = Instantiate(skillParticle[index], groundEffectPosition.position, groundEffectPosition.rotation);

        Destroy(effect, 5f);
    }

    public void SkillReset()
    {
        GetComponent<PlayerMovement>().enabled = true;
        skillFlag = false;
        index = 0;
        transform.parent = null;
        VFX.transform.parent = transform;
    }
}
