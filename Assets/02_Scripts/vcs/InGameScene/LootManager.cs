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
    [SerializeField] private Reward Debug_Reward;
    
    [SerializeField] private Transform DebugPos;

    [SerializeField] private float debugPower;

    [SerializeField] private DropItem[] HiddenItem;
    public LevelManager _LevelSystem { get; private set; }

    private void Awake()
    {
        _LevelSystem = FindObjectOfType<LevelManager>();
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
        int exp1 = (int)(expAmount * GameManager.Instance.Phase);
        int exp50 = NumberByUnit(ref exp1, 50);
        int exp10 = NumberByUnit(ref exp1, 10);
        int exp5 = NumberByUnit(ref exp1, 5);
        
        void Generate(GameObject expPrefab)
        {
            DropExpObject dropExpObject = Instantiate(expPrefab, generatePos, Quaternion.identity).GetComponent<DropExpObject>();
            Vector2 randomPosition = Random.insideUnitCircle.normalized;
            dropExpObject.AddForce(randomPosition, power);
        }
        //PoolManager.Instance.GetObject()
        for (int i = 0; i < exp1; i++)
        {
            Generate(DropExp1Prefab);
        }
        for (int i = 0; i < exp5; i++)
        {
            Generate(DropExp5Prefab);
        }
        for (int i = 0; i < exp10; i++)
        {
            Generate(DropExp10Prefab);
        }
        for (int i = 0; i < exp50; i++)
        {
            Generate(DropExp50Prefab);
        }
        
        
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
        if (Random.Range(0f, 100f) < dropItem.percentage)
        {
            DropItemObject dropItemObject = Instantiate(DropItemPrefab, generatePos, Quaternion.identity).GetComponent<DropItemObject>();
            dropItemObject.SetInfo(dropItem);
            Vector2 randomPosition = Random.insideUnitCircle.normalized;
            dropItemObject.AddForce(randomPosition, power);
        }
        
        SpawnHiddenReward(generatePos);
        
    }

    /**
     * <param name="reward">
     * 보상 내용이 포함된 Reward 구조체
     * </param>
     * <param name="generatePos">
     * 생성할 위치
     * </param>
     * <param name="power">
     * 생성 위치에서 드롭템들이 퍼져나갈 세기
     * </param>
     * <summary>
     * 필드에 Reward를 모두 생성해주는 메서드
     * </summary>
     */
    public void GenerateReward(Reward reward, Vector2 generatePos, float power = 1)
    {
        GenerateExp(reward.expAmount, generatePos, power);
        foreach (DropItem dropItem in reward.DropItems)
        {
            GenerateItem(dropItem, generatePos, power);
        }
    }
    
    
    /**
     * <param name="reward">
     * 보상 내용이 포함된 Reward 구조체
     * </param>
     * <param name="generatePos">
     * 생성할 위치
     * </param>
     * <param name="power">
     * 생성 위치에서 드롭템들이 퍼져나갈 세기
     * </param>
     * <summary>
     * 필드에 Reward를 모두 생성해주는 메서드
     * </summary>
     */
    public void GenerateRandomReward(Reward reward, Vector2 generatePos, float power = 1)
    {
        GenerateExp(reward.expAmount, generatePos, power);
        foreach (DropItem dropItem in reward.DropItems)
        {
            GenerateItem(dropItem, generatePos, power);
        }
    }
    
    [ContextMenu("Debug_GenerateItem")]
    public void DebugGen()
    {
        GenerateReward(Debug_Reward, DebugPos.position, debugPower);
    }

    private void SpawnHiddenReward(Vector2 pos)
    {
        foreach (DropItem dropItem in HiddenItem)
        {
            if (Random.Range(0f, 100f) < dropItem.percentage)
            {
                GenerateItem(dropItem, pos, 0);
            }
        }
    }



}
