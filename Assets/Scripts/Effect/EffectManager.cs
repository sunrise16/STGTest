#region USING
using System.Collections.Generic;
using UnityEngine;
#endregion

public class EffectPool : IPoolBase
{
    #region VARIABLE
    private List<GameObject> pEffectList;
    private Transform pEffectParent;
    private Transform pActiveEffectParent;
    #endregion

    #region CONSTRUCTOR
    public EffectPool(Transform pEffectParent, Transform pActiveEffectParent)
    {
        pEffectList = new List<GameObject>();
        this.pEffectParent = pEffectParent;
        this.pActiveEffectParent = pActiveEffectParent;
    }
    #endregion

    #region COMMON METHOD
    public GameObject AddPool()
    {
        GameObject pEffectObject = GameObject.Instantiate(Resources.Load(GlobalData.szEffectPrefabPath)) as GameObject;
        Transform pTransform = pEffectObject.GetComponent<Transform>();

        pEffectList.Add(pEffectObject);
        pTransform.position = Vector2.zero;
        pTransform.rotation = Quaternion.identity;
        pTransform.localScale = Vector3.one;
        pTransform.parent = pEffectParent;
        pEffectObject.SetActive(false);
        return pEffectObject;
    }
    public void ReturnPool(GameObject pEffectObject)
    {
        Transform pTransform = pEffectObject.GetComponent<Transform>();
        EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();
        EffectBase pEffectBase = pEffectMain.GetEffectBase();

        pEffectList.Add(pEffectObject);
        pTransform.parent = pEffectParent;
        pEffectBase.GetSpriteRenderer().color = new Color(1, 1, 1, 0);
        pEffectBase.GetSpriteRenderer().sprite = null;
        pEffectBase.GetAnimator().runtimeAnimatorController = null;
        pEffectMain.pStartDelegate = null;
        pEffectMain.pCommonDelegate = null;
        pEffectMain.pConditionDelegate = null;
        pEffectBase.AllReset();
        if (pTransform.childCount > 1)
        {
            for (int i = 1; i < pTransform.childCount; i++)
            {
                if (pTransform.GetChild(i).GetComponent<BulletMain>())
                {
                    BulletManager.Instance.GetBulletPool().ReturnPool(pTransform.GetChild(i).gameObject);
                }
                else if (pTransform.GetChild(i).GetComponent<EffectMain>())
                {
                    EffectManager.Instance.GetEffectPool().ReturnPool(pTransform.GetChild(i).gameObject);
                }
                else if (pTransform.GetChild(i).GetComponent<EnemyMain>())
                {
                    EnemyManager.Instance.GetEnemyPool().ReturnPool(pTransform.GetChild(i).gameObject);
                }
                i--;
            }
        }
    }
    public GameObject ExtractEffect(Vector3 vSpawnPosition, Vector3 vScale, Color pColor, EEffectType enEffectType, EEffectAnimationType enEffectAnimationType)
    {
        GameObject pEffectObject = pEffectList.Count.Equals(0) ? AddPool() : pEffectList[0];
        Transform pTransform = pEffectObject.GetComponent<Transform>();
        EffectMain pEffectMain = pEffectObject.GetComponent<EffectMain>();

        pEffectMain.Init(pEffectObject, pTransform, vSpawnPosition, vScale, pColor, enEffectType, enEffectAnimationType);
        pTransform.parent = pActiveEffectParent;
        pEffectList.RemoveAt(0);
        return pEffectObject;
    }
    #endregion
}

public class EffectManager : Singleton<EffectManager>
{
    #region VARIABLE
    private EffectPool pEffectPool;
    #endregion

    #region GET METHOD
    public EffectPool GetEffectPool() { return pEffectPool; }
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        Transform pEffectParent = GameObject.Find("EffectPool").GetComponent<Transform>();
        Transform pActiveEffectParent = GameObject.Find("ActiveEffects").GetComponent<Transform>();
        pEffectPool = new EffectPool(pEffectParent, pActiveEffectParent);
    }
    #endregion
}