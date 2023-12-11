using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
   /**
    * <summary>
    * 아이템 식별용 아이디
    * </summary>
    */
   public int id;
   /**
   * <summary>
   * 아이템 이름
   * </summary>
   */
   public string itemName;
   /**
    * <summary>
    * 아이템의 스프라이트 이름
    * </summary>
   */
   public int itemSpriteName;
   /**
    * <summary>
    * 아이템 설명
    * </summary>
    */
   public string description;
   /**
    * <summary>
    * 무제한, 또는 내구도가 존재하지 않는 아이템인가?
    * </summary>
    */
   public bool isLimited;
   
   /**
    * <summary>
    * 내구도 최대치 제한
    * </summary>
    */
   public float maxDurability;

   /**
    * <summary>
    * 한 슬롯당 최대치
    * </summary>
    */
   public int SlotSetAmount;

}
