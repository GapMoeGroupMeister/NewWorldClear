using UnityEngine;

[CreateAssetMenu(fileName = "FoodSO", menuName = "SO/FoodSO")]
public class FoodSO : ScriptableObject
{
    public int foodID;
    public float HP;
    public float hunger;
    public float thirsty;
}
