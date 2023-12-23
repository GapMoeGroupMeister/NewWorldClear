using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoSingleton<LootManager>
{

    [SerializeField] private GameObject DropItemPrefab;
    [SerializeField] private GameObject DropExpPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
    public void GenerateItem(DropItem dropItem, Vector2 generatePos, float power)
    {
        
    }
}
