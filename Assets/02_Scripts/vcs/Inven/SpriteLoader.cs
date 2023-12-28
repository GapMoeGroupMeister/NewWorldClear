using System;
using JetBrains.Annotations;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{

    public static Sprite[] spriteBase;

    private void Awake()
    {
        Load();
    }

    [ContextMenu("Custom/SpriteLoad")]
    public static void Load()
    {
        AssetBundle bundle = AssetBundle.LoadFromFile("Assets/AssetBundles/itemicon");
        spriteBase = bundle.LoadAllAssets<Sprite>();


    }
    [CanBeNull]
    public static Sprite FindSprite(string spriteName)
    {
        if (spriteBase == null)
        {
            Load();
        }
        foreach(Sprite sprite in spriteBase)
        {
            if (sprite.name == spriteName)
            {
                return sprite;
            }

        }

        return null;
    }

    [ContextMenu("Custom/SpriteCheckLoad")]
    public static void LoadCheck()
    {
        Debug.Log("spriteBase,Length: "+spriteBase.Length);
        foreach (Sprite sprite in spriteBase)
        {
            Debug.Log("sprite : ["+sprite.ToString()+"]");
        }
    }
}