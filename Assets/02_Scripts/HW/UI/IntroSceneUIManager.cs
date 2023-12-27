using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class IntroSceneUIManager : MonoBehaviour
{
    TextMeshProUGUI _dayText;
    Transform _pressText;
    int _day;
    SaveInfo _saveInfo;
    Image _blackPanel;

    private void Awake()
    {
        _dayText = transform.Find("DayText").GetComponent<TextMeshProUGUI>();
        _pressText = transform.Find("Press");
        _blackPanel = transform.Find("BlackPanel").GetComponent<Image>();
        _day = 18;
    }

    private void Start()
    {
        Init();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            _pressText.GetComponent<TextMeshProUGUI>().DOFade(0, 0.5f);
            StartCoroutine(IEDayText());
            StartCoroutine(UnderBarOnOff());
            DBManager.Save_userInfo(_saveInfo);
        }
    }

    private void Init()
    {
        _saveInfo = DBManager.Get_UserInfo();
        _day = _saveInfo.day + 1;
        
    }

    IEnumerator IEDayText()
    {
        yield return new WaitForSeconds(1.2f);
        string str = _day + "ÀÏ Â°.";
        char[] chars = str.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            string[] strs = _dayText.text.Split("_");
            _dayText.text = strs[0] + chars[i];
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.7f);
        _blackPanel.DOFade(1, 1.2f).OnComplete(() => SceneManager.LoadScene("StartScene"));
    }

    IEnumerator UnderBarOnOff()
    {
        while(true)
        {
            _dayText.text += "_";
            yield return new WaitForSeconds(0.4f);
            string[] strs = _dayText.text.Split("_");
            _dayText.text = strs[0];
            yield return new WaitForSeconds(0.4f);
        }
    }
}
