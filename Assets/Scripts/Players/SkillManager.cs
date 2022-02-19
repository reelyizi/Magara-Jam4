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
    [SerializeField] private Transform rightHandEffectPosition;
    [SerializeField] private Transform leftHandEffectPosition;
    [SerializeField] private Transform head;
    [SerializeField] private Transform middleFrontCharacter;

    private float elapsedTime;

    private bool skillFlag;
    private Skill cloneSkill = null;
    private int index = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !skillFlag && !SkillCooldownManagar._instance.bound.Contains(skillSlots[0]))
        {
            cloneSkill = skillSlots[0].GetComponent<SkillSlot>().skillObject;
            StartAnimations();
            GenerateSkill(0);
        }
        else if (Input.GetKeyDown(KeyCode.W) && !skillFlag && !SkillCooldownManagar._instance.bound.Contains(skillSlots[1]))
        {
            cloneSkill = skillSlots[1].GetComponent<SkillSlot>().skillObject;
            StartAnimations();
            GenerateSkill(1);
        }
        else if (Input.GetKeyDown(KeyCode.E) && !skillFlag && !SkillCooldownManagar._instance.bound.Contains(skillSlots[2]))
        {
            cloneSkill = skillSlots[2].GetComponent<SkillSlot>().skillObject;
            StartAnimations();
            GenerateSkill(2);
        }
        else if (Input.GetKeyDown(KeyCode.A) && !skillFlag)
        {
            Debug.Log("D");
        }
        else if (Input.GetKeyDown(KeyCode.S) && !skillFlag)
        {
            Debug.Log("E");
        }
        else if (Input.GetKeyDown(KeyCode.D) && !skillFlag)
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
        else if (skillPositionTypes[index] == SkillPositionType.leftHand)
            effect = Instantiate(skillParticle[index], leftHandEffectPosition.position, leftHandEffectPosition.rotation);
        else if (skillPositionTypes[index] == SkillPositionType.rightHand)
            effect = Instantiate(skillParticle[index], rightHandEffectPosition.position, rightHandEffectPosition.rotation);
        else if (skillPositionTypes[index] == SkillPositionType.head)
            effect = Instantiate(skillParticle[index], head.position, groundEffectPosition.rotation);
        else if (skillPositionTypes[index] == SkillPositionType.middleFrontCharacter)
            effect = Instantiate(skillParticle[index], middleFrontCharacter.position, middleFrontCharacter.rotation);

        Destroy(effect, 5f);
    }

    private void StartAnimations()
    {
        VFX.transform.parent = null;
        transform.parent = VFX.transform;

        Animator characterAnimator = VFX.GetComponent<Animator>();
        characterAnimator.SetTrigger("Skill");
        characterAnimator.SetInteger("Skill Type", cloneSkill.skillType);
        characterAnimator.SetInteger("Skill Level", cloneSkill.skillLevel);
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
