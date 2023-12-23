using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoSingleton<LootManager>
{

    [SerializeField] private GameObject DropItemPrefab;
    [SerializeField] private GameObject DropExpPrefab;


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
