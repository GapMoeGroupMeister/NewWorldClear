using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
	/**
	 * <summary>
	 * 플레이어의 체력
	 * Max값은
	 * </summary>
	 */
	public int health = 100;
	/**
	 * <summary>
	 * 플레이어의 포만감
	 * Max: 100
	 * </summary>
	 */
	public int hungry = 100;
	/**
	 * <summary>
	 * 플레이어의 갈증
	 * Max: 100
	 * </summary>
	 */
	public int thirsty = 100;

	/**
	 * <summary>
	 * 체력 레벨
	 * </summary>
	 */
	public int levelHealth = 1;

	/**
	 * <summary>
	 * 기본 공격력
	 * </summary>
	 */
	public int atk = 10;
	/**
	 * <summary>
	 * 기본 방어력
	 * </summary>
	 */
	public int def = 1;
	/**
	 * <summary>
	 * 스태미너
	 * </summary>
	 */
	public int stamina = 200;
	/**
	 * <summary>
	 * 기본 이동 속도
	 * </summary>
	 */
	public float moveSpeed = 1;
	/**
	 * <summary>
	 * 인벤토리 슬롯 사이즈
	 * </summary>
	 */
	public int inventorySize = 10;
	

	public static int Calc_HealthMax(int level)
	{
		int max = level * 5 + 100;
		return max;
	}

	public PlayerStatus()
	{
		health = 100;
		hungry = 100;
		thirsty = 100;
		
	}
 


}