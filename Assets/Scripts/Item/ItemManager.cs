#region USING
using System.Collections.Generic;
using UnityEngine;
#endregion

public class ItemPool : IPoolBase
{
    #region VARIABLE
    private List<GameObject> pItemList;
    private Transform pItemParent;
    private Transform pActiveItemParent;
    #endregion

    #region CONSTRUCTOR
    public ItemPool(Transform pItemParent, Transform pActiveItemParent)
    {
        pItemList = new List<GameObject>();
        this.pItemParent = pItemParent;
        this.pActiveItemParent = pActiveItemParent;
    }
    #endregion

    #region COMMON METHOD
    public GameObject AddPool()
    {
        GameObject pItemObject = GameObject.Instantiate(Resources.Load(GlobalData.szItemPrefabPath)) as GameObject;
        Transform pTransform = pItemObject.GetComponent<Transform>();

        pItemList.Add(pItemObject);
        pTransform.position = Vector2.zero;
        pTransform.rotation = Quaternion.identity;
        pTransform.localScale = Vector3.one;
        pTransform.parent = pItemParent;
        pItemObject.SetActive(false);
        return pItemObject;
    }
    public void ReturnPool(GameObject pItemObject)
    {
        Transform pTransform = pItemObject.GetComponent<Transform>();
        ItemMain pItemMain = pItemObject.GetComponent<ItemMain>();
        ItemBase pItemBase = pItemMain.GetItemBase();

        pItemList.Add(pItemObject);
        pTransform.parent = pItemParent;
        pItemBase.GetSpriteRenderer().color = new Color(1, 1, 1, 0);
        pItemBase.GetSpriteRenderer().sprite = null;
        pItemBase.GetAnimator().runtimeAnimatorController = null;
        pItemMain.pEvent -= ItemManager.Instance.ActiveAutoCollect;
        pItemMain.pCommonDelegate = null;
        pItemBase.AllReset();
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
                else if (pTransform.GetChild(i).GetComponent<ItemMain>())
                {
                    ItemManager.Instance.GetItemPool().ReturnPool(pTransform.GetChild(i).gameObject);
                }
                i--;
            }
        }
    }
    public GameObject ExtractItem(Vector3 vSpawnPosition, Vector3 vScale, Color pColor, EItemType enItemType, float fPadding, bool bSpriteRotate = false)
    {
        GameObject pItemObject = pItemList.Count.Equals(0) ? AddPool() : pItemList[0];
        Transform pTransform = pItemObject.GetComponent<Transform>();
        ItemMain pItemMain = pItemObject.GetComponent<ItemMain>();

        pItemMain.Init(pItemObject, pTransform, vSpawnPosition, vScale, pColor, enItemType, fPadding, bSpriteRotate);
        pItemMain.pEvent += ItemManager.Instance.ActiveAutoCollect;
        pTransform.parent = pActiveItemParent;
        pItemList.RemoveAt(0);
        return pItemObject;
    }
    #endregion
}

public class ItemManager : Singleton<ItemManager>
{
    #region VARIABLE
    private ItemPool pItemPool;
    #endregion

    #region GET METHOD
    public ItemPool GetItemPool() { return pItemPool; }
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        Transform pItemParent = GameObject.Find("ItemPool").GetComponent<Transform>();
        Transform pActiveItemParent = GameObject.Find("ActiveItems").GetComponent<Transform>();
        pItemPool = new ItemPool(pItemParent, pActiveItemParent);
    }
    public void ActiveAutoCollect(GameObject pItemObject)
    {
        ItemBase pItemBase = pItemObject.GetComponent<ItemMain>().GetItemBase();
        pItemBase.SetAutoCollect(true);
    }
    #endregion
}