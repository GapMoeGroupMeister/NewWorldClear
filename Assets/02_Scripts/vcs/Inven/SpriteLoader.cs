using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class SpriteLoader : MonoSingleton<SpriteLoader>
{

    public Sprite[] spriteBase;

    [MenuItem("Custom/SpriteLoad")]
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
            if (sprite.ToString() == spriteName)
            {
                return sprite;
            }
        }

        return null;
    }

    [MenuItem("Custom/SpriteCheckLoad")]
    public void LoadCheck()
    {
        Debug.Log("spriteBase,Length: "+spriteBase.Length);
        foreach (Sprite sprite in spriteBase)
        {
            Debug.Log("sprite : ["+sprite.ToString()+"]");
        }
    }
}
