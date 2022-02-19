using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textMeshProUGUI;
    public float lifeTime=.6f;
    public float minDist= 2f;
    public float maxDist=3f;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;
    void Start()
    {
        transform.LookAt(2*transform.position-Camera.main.transform.position);

        float direction=Random.rotation.eulerAngles.z;
        iniPos=transform.position+new Vector3(0,2,0);
        float dist = Random.Range(minDist,maxDist);
        targetPos=iniPos+Quaternion.Euler(0,0,direction)* new Vector3(dist,dist,0);
        transform.localScale=new Vector3(0,5,0);
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        float fraction=lifeTime/2f;



        if(timer>lifeTime) Destroy(gameObject);
        else if(timer>fraction) textMeshProUGUI.color=Color.Lerp(textMeshProUGUI.color,Color.clear,(timer-fraction)/(lifeTime-fraction));
        transform.position=Vector3.Lerp(iniPos,targetPos,Mathf.Sin(timer/lifeTime));
        transform.localScale=Vector3.Lerp(Vector3.zero,Vector3.one,Mathf.Sin(timer/lifeTime));

    }
    public void SetDamageText(int damage)
    {
        textMeshProUGUI.text=damage.ToString();
    }
}
