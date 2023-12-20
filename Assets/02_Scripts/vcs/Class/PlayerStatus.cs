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
	public int health;
	/**
	 * <summary>
	 * 플레이어의 포만감
	 * Max: 100
	 * </summary>
	 */
	public int hungry;
	/**
	 * <summary>
	 * 플레이어의 갈증
	 * Max: 100
	 * </summary>
	 */
	public int thirsty;

	/**
	 * <summary>
	 * 체력 레벨
	 * </summary>
	 */
	public int levelHealth;

	public static int Calc_HealthMax(int level)
	{
		int max = level * 5 + 100;
		return max;
	}



}