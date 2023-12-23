using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoSingleton<LootManager>
{
    [Header("DropItemPrefab")]
    [SerializeField] private GameObject DropItemPrefab;
    [Header("DropExpPrefabs")]
    [SerializeField] private GameObject DropExp1Prefab;
    [SerializeField] private GameObject DropExp5Prefab;
    [SerializeField] private GameObject DropExp10Prefab;
    [SerializeField] private GameObject DropExp50Prefab;

    [Header("Debug")]
    [SerializeField] private Transform DebugPos;

    [SerializeField] private DropItem debugItem;
    [SerializeField] private DropItem debugItem2;
    [SerializeField] private DropItem debugItem3;

    [SerializeField] private float debugPower;
    
    
    
    /**
     * <param name="generatePos">
     * 경험치 구슬을 생성할 위치
     * </param>
     * <param name="power">
     * 생성위치에서 랜덤하게 퍼져나갈 세기
     * </param>
     * <summary>
     * 경험치구슬을 생성하는 메서드
     * </summary>
     */
    public void GenerateExp(int expAmount, Vector2 generatePos, float power)
    {
        int exp1 = expAmount;
        int exp50 = NumberByUnit(ref exp1, 50);
        int exp10 = NumberByUnit(ref exp1, 10);
        int exp5 = NumberByUnit(ref exp1, 5);
        
        //PoolManager.Instance.GetObject()
        
        DropItemObject dropItemObject = Instantiate(DropItemPrefab, generatePos, Quaternion.identity).GetComponent<DropItemObject>();
        Vector2 randomPosition = Random.insideUnitCircle.normalized;
        dropItemObject.AddForce(randomPosition, power);
    }

    /**
     * <param name="num">
     * 남은 값 (ref 참조형태)
     * </param>
     * <param name="unit">
     * num값을 나누는 단위
     * </param>
     * <summary>
     * 입력받은 num값을 단위로 나누어 반환하고 나머지를 저장하는 메서드
     * </summary>
     * <returns>
     * 단위로 나눈 값을 반환
     * </returns>
     */
    private int NumberByUnit(ref int num, int unit)
    {
        int numByUnit = num / unit;
        num %= unit;
        return numByUnit;
    }
    
    /**
     * <param name="dropItem">
     * 떨어진 아이템 속성
     * </param>
     * <param name="generatePos">
     * 아이템 드롭템을 생성할 위치
     * </param>
     * <param name="power">
     * 생성위치에서 랜덤하게 퍼져나갈 세기
     * </param>
     * <summary>
     * 아이템 드롭템을 생성하는 메서드
     * </summary>
     */
    public void GenerateItem(DropItem dropItem, Vector2 generatePos, float power = 1)
    {
        DropItemObject dropItemObject = Instantiate(DropItemPrefab, generatePos, Quaternion.identity).GetComponent<DropItemObject>();
        dropItemObject.SetInfo(dropItem);
        Vector2 randomPosition = Random.insideUnitCircle.normalized;
        dropItemObject.AddForce(randomPosition, power);
        
    }
    
    [ContextMenu("Debug_GenerateItem")]
    public void DebugGen()
    {
        GenerateItem(debugItem,DebugPos.position, debugPower);
        GenerateItem(debugItem2,DebugPos.position, debugPower);
        GenerateItem(debugItem3,DebugPos.position, debugPower);
    }
}
