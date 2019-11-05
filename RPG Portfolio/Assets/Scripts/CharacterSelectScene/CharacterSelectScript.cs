﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectScript : MonoBehaviour
{
    [SerializeField] Sprite[] PirateSKILL = new Sprite[3]; // 해적스킬 아이콘들
    [SerializeField] GameObject[] BabarianSKILL = new GameObject[3];
    [SerializeField] GameObject[] WizardSKILL = new GameObject[3];
    [SerializeField]
    GameObject UIinitiliazer;

    [SerializeField]
    GameObject[] Managers = new GameObject[10]; // 0: Scene ,1 : Inventory ,2 : Data ,3: Player ,4 : Interface ,5 : Skill , 6: Game

    delegate void SaveJob(string _in);
    delegate string LoadJob();
    delegate void ChangeScene();
    delegate void ui_setting();

    private SaveJob sj;
    private LoadJob lj;
    private PlayerManagerScripts PM;
    private SceneManagerScript SM;
    [SerializeField]
    Camera camera;

    [SerializeField]
    GameObject job;

    [SerializeField]
    GameObject[] skill = new GameObject[3]; // 0 : 1번스킬  1 : 2번 스킬 2 : 3번 스킬 버튼들 ...

    [SerializeField]
    GameObject skill_description;

    [SerializeField]
    InputField nickname;

    

    int count = 0;
    Vector3 initPosition;

    [SerializeField]
    GameObject previous;

    [SerializeField]
    GameObject next;

    void Start()
    {

  
        PM = Managers[(int)Enum.Managerlist.Player].GetComponent<PlayerManagerScripts>();
        SM = Managers[(int) Enum.Managerlist.Scene].GetComponent<SceneManagerScript>();

        initPosition.x = -9.16f; // 카메라의 초기값 ( 캐릭터를 바라보는 위치 )
        initPosition.y = 2.33f;
        initPosition.z = 0.58f;
        job.GetComponent<Text>().text = "해적";

        for (int i = 0; i < 3; i++)
            skill[i].transform.GetComponent<Image>().sprite = PirateSKILL[i];

        skill_description.transform.GetComponent<Text>().text = "기본적인 해적의 근접공격 스킬, 연속으로 사용시 연타모션을 취한다.";
        
        camera.transform.position = initPosition;

        previous.gameObject.SetActive(false);

     
    }



    public void nextPosition()
    {
        previous.gameObject.SetActive(true);
        camera.transform.position += new Vector3(4.5f, 0f, 0f);
        count++;

        switch (count)
        {
            case 0:
                job.GetComponent<Text>().text = "해적";
                
                break;

            case 1:
                job.GetComponent<Text>().text = "바바리안";
                break;

            case 2:
                job.GetComponent<Text>().text = "메이지";
                break;
        }

        

        if (count == 2)
        {
            next.gameObject.SetActive(false); // 버튼 비활성화
        }
        else
        {
            next.gameObject.SetActive(true);
        }


    }

    public void previousPosition()
    {

        next.gameObject.SetActive(true);
        camera.transform.position -= new Vector3(4.5f, 0f, 0f);
        count--;

        switch (count)
        {
            case 0:
                job.GetComponent<Text>().text = "해적";
                break;

            case 1:
                job.GetComponent<Text>().text = "바바리안";
                break;

            case 2:
                job.GetComponent<Text>().text = "메이지";
                break;
        }

        if (count == 0)
        {
            previous.gameObject.SetActive(false); //버튼 비활성화
        }
        else
        {
            previous.gameObject.SetActive(true);
        }

    }

    public void selectbtn()
    {

        UIinitiliazer.GetComponent<UIInitialize>().UISetOn();
        switch (count)
        {
            case 0 :
                sj = PM.Save_Job;

                sj("Pirate");
                Managers[4].GetComponent<InterfaceManagerScript>().SetUnitPortrait("Pirate");
                break;

            case 1:
                sj = PM.Save_Job;
                sj("Barbarian");
                break;

            case 2:
                sj = PM.Save_Job;
                sj("Wizard");
                break;
        }

        Managers[3].transform.GetComponent<PlayerManagerScripts>().Save_Name(nickname.text);
      
       
        SM.EnterStartChurch();
   
        // FadeIn 후 씬이동... 추가하기...
    }

    public void SKILL1INFO()
    {
       switch(count)
        {
            case 0:
                skill_description.transform.GetComponent<Text>().text = "기본적인 해적의 근접공격 스킬, 연속으로 사용시 연타모션을 취한다.";
                break;

            case 1:
                skill_description.transform.GetComponent<Text>().text = "기본적인 바바리안의 근접공격 스킬";
                break;

            case 2:
                skill_description.transform.GetComponent<Text>().text = "마법사의 기본 공격 스킬";
                break;
                
        }
    }

    public void SKILL2INFO()
    {
        switch (count)
        {
            case 0:
                skill_description.transform.GetComponent<Text>().text = "해적의 버프스킬";
                break;

            case 1:
                skill_description.transform.GetComponent<Text>().text = "바바리안의 버프스킬";
                break;

            case 2:
                skill_description.transform.GetComponent<Text>().text = "마법사의 버프스킬";
                break;

        }
    }

    public void SKILL3INFO()
    {

    }
}
