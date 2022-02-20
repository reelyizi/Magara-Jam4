using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SkillCooldownManagar : MonoBehaviour
{
    public static SkillCooldownManagar _instance;

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

    public List<GameObject> bound = new List<GameObject>();
    public List<float> cooldown = new List<float>();
    List<float> startCooldown = new List<float>();

    void Update()
    {
        if (bound.Any() && GameManager.instance.playStatus == GameManager.PlayStatus.ingame)
        {
            for (int i = 0; i < cooldown.Count; i++)
            {
                cooldown[i] -= Time.deltaTime;
                bound[i].transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = 1 - (startCooldown[i] - cooldown[i]) / startCooldown[i];
                if (cooldown[i] < 0)
                {
                    bound[i].transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = 0;
                    cooldown.RemoveAt(i);
                    startCooldown.RemoveAt(i);
                    bound.RemoveAt(i);
                }
            }
        }
    }

    public void AddCooldown(GameObject keyObject, float coolDown)
    {
        bound.Add(keyObject);
        this.cooldown.Add(coolDown);
        startCooldown.Add(coolDown);
    }
}
