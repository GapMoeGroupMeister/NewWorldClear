using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveInfo
{
    /**
     * <summary>
     * 생존일
     * </summary>
     */
    public int day;
    /**
     * <summary>
     * 주인공 이름, 유저 입력
     * </summary>
     */
    public string userName;

    /**
     * <summary>
     * 처음 플레이 하는가?
     * </summary>
     */
    public bool isFirstPlay;

    /**
     * <summary>
     * 남은 모험 횟수
     * </summary>
     */
    public int adventureCount;
    
    /**
     * <summary>
     * 현재 퀘스트 id
     * </summary>
     */
    public int nowRequestId;

    /**
     * <summary>
     *
     * </summary>
     */
    public int nowDay;
}
