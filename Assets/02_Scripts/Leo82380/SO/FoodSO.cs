using UnityEngine;

[CreateAssetMenu(fileName = "FoodSO", menuName = "SO/FoodSO")]
public class FoodSO : ScriptableObject
{
    public int foodID;
    public int HP;
    public int hunger;
    public int thirsty;
}
