using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngamePlayButton : MonoBehaviour
{
    public GameObject play;

    public void CloseText()
    {
        play.SetActive(false);
    }
}
