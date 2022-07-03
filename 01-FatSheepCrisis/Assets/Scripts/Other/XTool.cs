using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XTool
{
    private static AssetBundle uiAB;
    private static AssetBundle textureAB;
    public static Vector3 RangeInsideCirclePosition(Vector3 _pos, float minR, float maxR)
    {
        Vector3 pos = _pos;
        while (Vector3.Distance(pos, _pos) < minR || Vector3.Distance(pos, _pos) > maxR)
        {
            pos = new Vector3(Random.Range(_pos.x + maxR, _pos.x - maxR), Random.Range(_pos.y + maxR, _pos.y - maxR), 0);
        }
        return pos;
    }
    public static T LoadAssetAtPath<T>(string path, string _asset_name) where T : UnityEngine.Object
    {
#if UNITY_EDITOR && !FORCE_USE_AB
        string asset = path + _asset_name;
        var ret = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(asset);
        if (ret == null)
        {
            Debug.LogError("can not find asset " + asset);
            return null;
        }
        return ret;
#endif
        return null;
    }

    public static float CalculateDamage(float aggressivity)
    {
        float _Aggressivity;
        if (UnityEngine.Random.value < TotalAttribute.CritChance)
        {
            _Aggressivity = aggressivity * (1 + TotalAttribute.CritDamage);
            Player.Instance.isCrit = true;
        }
        else
        {
            _Aggressivity = aggressivity;
        }
        return _Aggressivity * (1 + TotalAttribute.FinalDamage) + TotalAttribute.AdditionalDamage;
    }

    public static GameObject LoadUIAssestBundle(string name)
    {
        if (uiAB == null)
        {
            uiAB = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/ui.bundle");
            return uiAB.LoadAsset<GameObject>(name);
        }
        else
        {
            return uiAB.LoadAsset<GameObject>(name);
        }
    }

    public static Sprite LoadTextureAssestBundle(string name)
    {
        if (textureAB == null)
        {
            textureAB = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/texture.bundle");
            return textureAB.LoadAsset<Sprite>(name);
        }
        else
        {
            return textureAB.LoadAsset<Sprite>(name);
        }
    }
}
