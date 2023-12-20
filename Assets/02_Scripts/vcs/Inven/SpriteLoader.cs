using System;
using JetBrains.Annotations;
using UnityEngine;

public class SpriteLoader : MonoSingleton<SpriteLoader>
{

    public Sprite[] spriteBase;

    private void Awake()
    {
        Load();
    }

    [ContextMenu("Custom/SpriteLoad")]
    public void Load()
    {
        AssetBundle bundle = AssetBundle.LoadFromFile("Assets/AssetBundles/itemicon");
        spriteBase = bundle.LoadAllAssets<Sprite>();


    }
    [CanBeNull]
    public Sprite FindSprite(string spriteName)
    {
        if (spriteBase == null)
        {
            Load();
        }
        foreach(Sprite sprite in spriteBase)
        {
            print(sprite.name);
            if (sprite.name == spriteName)
            {
                return sprite;
            }

        }

        return null;
    }

    [ContextMenu("Custom/SpriteCheckLoad")]
    public void LoadCheck()
    {
        Debug.Log("spriteBase,Length: "+spriteBase.Length);
        foreach (Sprite sprite in spriteBase)
        {
            Debug.Log("sprite : ["+sprite.ToString()+"]");
        }
    }
}