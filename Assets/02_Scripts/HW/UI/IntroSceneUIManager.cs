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
    Transform _finalAnswerTrm;
    bool isStart = false;

    private void Awake()
    {
        _dayText = transform.Find("DayText").GetComponent<TextMeshProUGUI>();
        _pressText = transform.Find("Press");
        _blackPanel = transform.Find("BlackPanel").GetComponent<Image>();
        _finalAnswerTrm = _blackPanel.transform.Find("Image");
    }

    private void Start()
    {
        Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isStart)
        {
            isStart = true;
            _pressText.GetComponent<TextMeshProUGUI>().DOFade(0, 0.5f);
            StartCoroutine(IEDayText());
            StartCoroutine(UnderBarOnOff(_dayText));
        }
    }

    private void Init()
    {
        _saveInfo = DBManager.Get_UserInfo();
        _day = _saveInfo.day + 1;
    }

    public void FinishEnter()
    {
        _finalAnswerTrm.gameObject.SetActive(true);
        string name = _blackPanel.transform.Find("InputField").GetComponent<TMP_InputField>().text;
        _finalAnswerTrm.Find("Text").GetComponent<TextMeshProUGUI>().text = $"<color=#ffff00><size=75>{name},\n<color=#000000><size=62>확실합니까?";
    }

    public void Yes()
    {
        _blackPanel.transform.Find("Panel").gameObject.SetActive(true);
        _blackPanel.transform.Find("Panel").GetComponent<Image>().DOFade(1, 1.2f).OnComplete(() => EnterScene());
        _saveInfo.isFirstPlay = false;
        DBManager.Save_userInfo(_saveInfo);
    }

    public void No()
    {
        _finalAnswerTrm.gameObject.SetActive(false);
    }

    public void EnterScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    IEnumerator IEDayText()
    {
        yield return new WaitForSeconds(1.2f);
        string str = _day + "일 째.";
        char[] chars = str.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            string[] strs = _dayText.text.Split("_");
            _dayText.text = strs[0] + chars[i];
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.7f);
        if (!_saveInfo.isFirstPlay)
        {
            _blackPanel.DOFade(1, 1.2f).OnComplete(() => SceneManager.LoadScene("StartScene"));
            yield break;
        }
        TextMeshProUGUI text = null;
        _blackPanel.DOFade(1, 1.2f).OnComplete(() =>
        {
            text = _blackPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            print(_blackPanel.transform.GetChild(0).name);
            text.gameObject.SetActive(true);
        });
        yield return new WaitForSeconds(1.2f);
        yield return null;
        StartCoroutine(UnderBarOnOff(text));
        string str2 = "당신의 이름은?";
        char[] chars2 = str2.ToCharArray();
        for (int i = 0; i < chars2.Length; i++)
        {
            string[] strs = text.text.Split("_");
            text.text = strs[0] + chars2[i];
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.7f);
        _blackPanel.transform.GetChild(1).gameObject.SetActive(true);
    }

    IEnumerator UnderBarOnOff(TextMeshProUGUI text)
    {
        while (true)
        {
            text.text += "_";
            yield return new WaitForSeconds(0.4f);
            string[] strs = text.text.Split("_");
            text.text = strs[0];
            yield return new WaitForSeconds(0.4f);
        }
    }
}
