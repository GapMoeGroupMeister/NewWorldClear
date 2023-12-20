using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{

    public static Sprite[] spriteBase;

    public static void Load()
    {
        AssetBundle bundle = AssetBundle.LoadFromFile("Assets/AssetBundles/itemicon");
        spriteBase = (Sprite[])bundle.LoadAllAssets();

        
    }

    [CanBeNull]
    public static Sprite FindSprite(string spriteName)
    {
        
        foreach(Sprite sprite in spriteBase)
        {
            if (sprite.ToString() == spriteName)
            {
                return sprite;
            }
        }

        return null;
    }
}
