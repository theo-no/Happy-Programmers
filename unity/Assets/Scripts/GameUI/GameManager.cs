using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;


    // Update is called once per frame
    public void Action(GameObject scanObj)
    {
        if(isAction)
        {
            isAction = false;
        } 
        else
        {
            isAction = true;
            scanObject = scanObj;
            talkText.text = "�̰��� �̸��� " + scanObj.name + "�̶�� �Ѵ�.";
        }

        talkPanel.SetActive(isAction);
    }
}
