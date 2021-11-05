#region USING
using System.Collections.Generic;
using UnityEngine;
#endregion

#region 탄막 구현에 대한 설정

//  * (Index 0) 빈 탄막
//  * (Index 1 ~ 16)    콩알탄                : Circle Collider 2D, Radius 0.02
//  * (Index 17 ~ 32)   기본 원탄             : Circle Collider 2D, Radius 0.04
//  * (Index 33 ~ 48)   테두리 원탄           : Circle Collider 2D, Radius 0.03
//  * (Index 49 ~ 64)   쌀탄                  : Capsule Collider 2D, Size X 0.025, Y 0.05, Direction Vertical
//  * (Index 65 ~ 80)   보석탄                : Capsule Collider 2D, Size X 0.02, Y 0.05, Direction Vertical
//  * (Index 81 ~ 96)   쿠나이탄              : Capsule Collider 2D, Size X 0.02, Y 0.055, Direction Vertical
//  * (Index 97 ~ 112)  쐐기탄                : Capsule Collider 2D, Size X 0.04, Y 0.06, Direction Vertical
//  * (Index 113 ~ 128) 부적탄                : Box Collider 2D, Size X 0.025, Y 0.05
//  * (Index 129 ~ 144) 총알탄                : Box Collider 2D, Size X 0.02, Y 0.06
//  * (Index 145 ~ 160) 감주탄                : Capsule Collider 2D, Size X 0.02, Y 0.07, Direction Vertical
//  * (Index 161 ~ 176) 소형 별탄             : Circle Collider 2D, Radius 0.015, Offset X 0, Y -0.005
//  * (Index 177 ~ 192) 옥구슬탄              : Circle Collider 2D, Radius 0.04
//  * (Index 193 ~ 208) 소형 테두리 원탄       : Circle Collider 2D, Radius 0.015
//  * (Index 209 ~ 224) 소형 쌀탄             : Circle Collider 2D, Radius 0.015
//  * (Index 225 ~ 240) 소형 촉탄             : Circle Collider 2D, Radius 0.02, Offset X 0, Y 0.01
//  * (Index 241 ~ 244) 대옥탄                : Circle Collider 2D, Radius 0.135
//  * (Index 245 ~ 247) 엽전탄                : Circle Collider 2D, Radius 0.04
//  * (Index 258 ~ 265) 대형 환탄             : Circle Collider 2D, Radius 0.08
//  * (Index 266 ~ 273) 나비탄                : Circle Collider 2D, Radius 0.02
//  * (Index 274 ~ 281) 나이프탄              : Capsule Collider 2D, Size X 0.035, Y 0.2, Direction Vertical
//  * (Index 282 ~ 289) 알약탄                : Capsule Collider 2D, Size X 0.06, Y 0.18, Direction Vertical
//  * (Index 290 ~ 297) 대형 별탄             : Circle Collider 2D, Radius 0.045, Offset X 0, Y -0.01
//  * (Index 298 ~ 305) 탄막 발사 이펙트
//  * (Index 306 ~ 313) 대형 발광탄           : Circle Collider 2D, Radius 0.1
//  * (Index 314 ~ 321) 하트탄                : Circle Collider 2D, Radius 0.065, Offset X 0, Y -0.01
//  * (Index 322 ~ 329) 탄막 발사 이펙트
//  * (Index 330 ~ 337) 화살탄                : Capsule Collider 2D, Size X 0.01, Y 0.2, Direction Vertical
//  * (Index 338 ~ 349) 음표탄                : Circle Collider 2D, Radius 0.02, Offset X -0.01, Y -0.09, Sprite Animation 적용
//  * (Index 350 ~ 357) 쉼표탄                : Capsule Collider 2D, Size X 0.02, Y 0.18
//  * (Index 358 ~ 373) 고정 레이저탄          : Box Collider 2D, Size X 0.09, Y 0.13
//  * (Index 374 ~ 389) 무빙 레이저탄 (머리 1) : Box Collider 2D, Size X 0.52, Y 0.04, Offset X 0.02, Y 0
//  * (Index 390 ~ 405) 무빙 레이저탄 (머리 2) : Box Collider 2D, Size X 0.56, Y 0.04
//  * (Index 406 ~ 421) 무빙 레이저탄 (몸통)   : Box Collider 2D, Size X 0.24, Y 0.04
//  * (Index 422 ~ 437) 무빙 레이저탄 (꼬리 1) : Box Collider 2D, Size X 0.56, Y 0.04
//  * (Index 438 ~ 453) 무빙 레이저탄 (꼬리 2) : Box Collider 2D, Size X 0.52, Y 0.04, Offset X -0.02, Y 0

#endregion

public class BulletPool : IPoolBase
{
    #region VARIABLE
    private List<GameObject> pBulletList;
    private Transform pBulletParent;
    private Transform pActiveBulletParent;
    private Transform pActivePlayerBulletParent;
    #endregion

    #region CONSTRUCTOR
    public BulletPool(Transform pBulletParent, Transform pActiveBulletParent, Transform pActivePlayerBulletParent)
    {
        pBulletList = new List<GameObject>();
        this.pBulletParent = pBulletParent;
        this.pActiveBulletParent = pActiveBulletParent;
        this.pActivePlayerBulletParent = pActivePlayerBulletParent;
    }
    #endregion

    #region GET METHOD
    /// <summary>
    /// * 주의 : 임시 테스트용 카운트 메소드입니다. 정식 UI 도입 시 지울 것
    /// </summary>
    public int GetBulletListCount(int iIndex)
    {
        switch (iIndex)
        {
            case 0:
                return pActiveBulletParent.childCount;
            case 1:
                return pBulletList.Count;
            default:
                break;
        }
        return 0;
    }
    #endregion

    #region COMMON METHOD
    public GameObject AddPool()
    {
        GameObject pBulletObject = GameObject.Instantiate(Resources.Load(GlobalData.szBulletPrefabPath)) as GameObject;
        Transform pTransform = pBulletObject.GetComponent<Transform>();

        pBulletList.Add(pBulletObject);
        pTransform.position = Vector2.zero;
        pTransform.rotation = Quaternion.identity;
        pTransform.localScale = Vector3.one;
        pTransform.parent = pBulletParent;
        pBulletObject.SetActive(false);
        return pBulletObject;
    }
    public void ReturnPool(GameObject pBulletObject)
    {
        Transform pTransform = pBulletObject.GetComponent<Transform>();
        BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
        BulletBase pBulletBase = pBulletMain.GetBulletBase();
        BoxCollider2D pBoxCollider = pBulletObject.GetComponent<BoxCollider2D>();
        CapsuleCollider2D pCapsuleCollider = pBulletObject.GetComponent<CapsuleCollider2D>();
        CircleCollider2D pCircleCollider = pBulletObject.GetComponent<CircleCollider2D>();

        pBulletList.Add(pBulletObject);
        pTransform.parent = pBulletParent;
        pBoxCollider.size = pBoxCollider.offset = Vector2.zero;
        pBoxCollider.enabled = false;
        pCapsuleCollider.size = pCapsuleCollider.offset = Vector2.zero;
        pCapsuleCollider.enabled = false;
        pCircleCollider.radius = 0.0f;
        pCircleCollider.offset = Vector2.zero;
        pCircleCollider.enabled = false;
        if (!pBulletBase.GetBulletType().Equals(EBulletType.enType_Empty))
        {
            pBulletBase.GetSpriteRenderer().color = Color.white;
            pBulletBase.GetSpriteRenderer().sprite = null;
            pBulletBase.GetAnimator().runtimeAnimatorController = null;
        }
        pBulletMain.pDelegateDictionary.Clear();
        pBulletBase.AllReset();
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
    public GameObject ExtractBullet(Vector3 vSpawnPosition, Vector3 vScale, EBulletType enBulletType, EBulletShooter enBulletShooter, EEnemyBulletType enEnemyBulletType,
        EPlayerBulletType enPlayerBulletType, float fBulletAlpha, float fPadding, bool bHoming = false, float fHomingSpeed = 600.0f)
    {
        GameObject pBulletObject = pBulletList.Count.Equals(0) ? AddPool() : pBulletList[0];
        Transform pTransform = pBulletObject.GetComponent<Transform>();
        
        if (enBulletShooter.Equals(EBulletShooter.enShooter_Player))
        {
            pTransform.parent = pActivePlayerBulletParent;
        }
        else if (enBulletShooter.Equals(EBulletShooter.enShooter_Enemy))
        {
            pTransform.parent = pActiveBulletParent;
        }
        pBulletList.RemoveAt(0);
        switch (enBulletType)
        {
            case EBulletType.enType_Box:
                return SettingBoxBullet(ref pBulletObject, ref pTransform, vSpawnPosition, vScale, enBulletShooter, enEnemyBulletType, enPlayerBulletType,
                    fBulletAlpha, fPadding, bHoming, fHomingSpeed);
            case EBulletType.enType_Capsule:
                return SettingCapsuleBullet(ref pBulletObject, ref pTransform, vSpawnPosition, vScale, enBulletShooter, enEnemyBulletType, enPlayerBulletType,
                    fBulletAlpha, fPadding, bHoming, fHomingSpeed);
            case EBulletType.enType_Circle:
                return SettingCircleBullet(ref pBulletObject, ref pTransform, vSpawnPosition, vScale, enBulletShooter, enEnemyBulletType, enPlayerBulletType,
                    fBulletAlpha, fPadding, bHoming, fHomingSpeed);
            default:
                return SettingBullet(ref pBulletObject, ref pTransform, vSpawnPosition, vScale, enBulletType, enBulletShooter, enEnemyBulletType,
                    enPlayerBulletType, fBulletAlpha, fPadding, bHoming, fHomingSpeed);
        }
    }
    public GameObject SettingBoxBullet(ref GameObject pBulletObject, ref Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale,
        EBulletShooter enBulletShooter, EEnemyBulletType enEnemyBulletType, EPlayerBulletType enPlayerBulletType, float fAlpha, float fPadding,
        bool bHoming = false, float fHomingSpeed = 600.0f)
    {
        BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
        if (enBulletShooter.Equals(EBulletShooter.enShooter_Player))
        {
            pBulletMain.Init(pBulletObject, pTransform, vSpawnPosition, vScale, EBulletType.enType_Box, enPlayerBulletType, fAlpha, fPadding, bHoming, fHomingSpeed);
        }
        else
        {
            pBulletMain.Init(pBulletObject, pTransform, vSpawnPosition, vScale, EBulletType.enType_Box, enEnemyBulletType, fAlpha, fPadding, bHoming, fHomingSpeed);
        }
        BulletBase pBulletBase = pBulletMain.GetBulletBase();

        if (enBulletShooter.Equals(EBulletShooter.enShooter_Player))
        {
            switch (enPlayerBulletType)
            {
                case EPlayerBulletType.enType_ReimuPrimary:
                    pBulletBase.GetBoxCollider().size = new Vector2(0.1f, 0.16f);
                    pBulletBase.GetBoxCollider().offset = new Vector2(0.0f, 0.2f);
                    break;
                case EPlayerBulletType.enType_ReimuSecondary_Homing:
                    pBulletBase.GetBoxCollider().size = new Vector2(0.1f, 0.15f);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (enEnemyBulletType)
            {
                case EEnemyBulletType.enType_BoxLaser:
                    pBulletBase.GetBoxCollider().size = new Vector2(0.07f, 0.14f);
                    break;
                case EEnemyBulletType.enType_CurvedLaser:
                    pBulletBase.GetBoxCollider().size = new Vector2(0.05f, 0.12f);
                    break;
                default:
                    break;
            }
        }
        return pBulletObject;
    }
    public GameObject SettingCapsuleBullet(ref GameObject pBulletObject, ref Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale,
        EBulletShooter enBulletShooter, EEnemyBulletType enEnemyBulletType, EPlayerBulletType enPlayerBulletType, float fAlpha, float fPadding,
        bool bHoming = false, float fHomingSpeed = 600.0f)
    {
        BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
        if (enBulletShooter.Equals(EBulletShooter.enShooter_Player))
        {
            pBulletMain.Init(pBulletObject, pTransform, vSpawnPosition, vScale, EBulletType.enType_Capsule, enPlayerBulletType, fAlpha, fPadding, bHoming, fHomingSpeed);
        }
        else
        {
            pBulletMain.Init(pBulletObject, pTransform, vSpawnPosition, vScale, EBulletType.enType_Capsule, enEnemyBulletType, fAlpha, fPadding, bHoming, fHomingSpeed);
        }
        BulletBase pBulletBase = pBulletMain.GetBulletBase();

        if (enBulletShooter.Equals(EBulletShooter.enShooter_Player))
        {
            switch (enPlayerBulletType)
            {
                case EPlayerBulletType.enType_ReimuSecondary_Niddle:
                    pBulletBase.GetCapsuleCollider().size = new Vector2(0.1f, 0.5f);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (enEnemyBulletType)
            {
                case EEnemyBulletType.enType_Arrowhead:
                    pBulletBase.GetCapsuleCollider().size = new Vector2(0.04f, 0.1f);
                    pBulletBase.GetCapsuleCollider().offset = new Vector2(0.0f, -0.01f);
                    break;
                case EEnemyBulletType.enType_Capsule:
                    pBulletBase.GetCapsuleCollider().size = new Vector2(0.025f, 0.07f);
                    pBulletBase.GetCapsuleCollider().offset = Vector2.zero;
                    break;
                case EEnemyBulletType.enType_Knife:
                    pBulletBase.GetCapsuleCollider().size = new Vector2(0.05f, 0.16f);
                    pBulletBase.GetCapsuleCollider().offset = new Vector2(0.0f, 0.02f);
                    break;
                default:
                    break;
            }
        }
        return pBulletObject;
    }
    public GameObject SettingCircleBullet(ref GameObject pBulletObject, ref Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale,
        EBulletShooter enBulletShooter, EEnemyBulletType enEnemyBulletType, EPlayerBulletType enPlayerBulletType, float fAlpha, float fPadding,
        bool bHoming = false, float fHomingSpeed = 600.0f)
    {
        BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
        if (enBulletShooter.Equals(EBulletShooter.enShooter_Player))
        {
            pBulletMain.Init(pBulletObject, pTransform, vSpawnPosition, vScale, EBulletType.enType_Circle, enPlayerBulletType, fAlpha, fPadding, bHoming, fHomingSpeed);
        }
        else
        {
            pBulletMain.Init(pBulletObject, pTransform, vSpawnPosition, vScale, EBulletType.enType_Circle, enEnemyBulletType, fAlpha, fPadding, bHoming, fHomingSpeed);
        }
        BulletBase pBulletBase = pBulletMain.GetBulletBase();

        if (enBulletShooter.Equals(EBulletShooter.enShooter_Player))
        {
            switch (enPlayerBulletType)
            {
                default:
                    break;
            }
        }
        else
        {
            switch (enEnemyBulletType)
            {
                case EEnemyBulletType.enType_DoubleCircle:
                    pBulletBase.GetCircleCollider().radius = 0.03f;
                    pBulletBase.GetCircleCollider().offset = Vector2.zero;
                    break;
                case EEnemyBulletType.enType_Circle:
                    pBulletBase.GetCircleCollider().radius = 0.025f;
                    pBulletBase.GetCircleCollider().offset = Vector2.zero;
                    break;
                case EEnemyBulletType.enType_TinyCircle:
                    pBulletBase.GetCircleCollider().radius = 0.02f;
                    pBulletBase.GetCircleCollider().offset = Vector2.zero;
                    break;
                case EEnemyBulletType.enType_Butterfly:
                    pBulletBase.GetCircleCollider().radius = 0.025f;
                    pBulletBase.GetCircleCollider().offset = Vector2.zero;
                    break;
                case EEnemyBulletType.enType_GhostCircle:
                    pBulletBase.GetCircleCollider().radius = 0.04f;
                    pBulletBase.GetCircleCollider().offset = new Vector2(0.0f, 0.035f);
                    break;
                case EEnemyBulletType.enType_LightCircle:
                    pBulletBase.GetCircleCollider().radius = 0.125f;
                    pBulletBase.GetCircleCollider().offset = Vector2.zero;
                    break;

                default:
                    break;
            }
        }
        return pBulletObject;
    }
    public GameObject SettingBullet(ref GameObject pBulletObject, ref Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale,
        EBulletType enBulletType, EBulletShooter enBulletShooter, EEnemyBulletType enEnemyBulletType, EPlayerBulletType enPlayerBulletType, float fAlpha,
        float fPadding, bool bHoming = false, float fHomingSpeed = 600.0f)
    {
        BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
        if (enBulletShooter.Equals(EBulletShooter.enShooter_Player))
        {
            pBulletMain.Init(pBulletObject, pTransform, vSpawnPosition, vScale, enBulletType, enPlayerBulletType, fAlpha, fPadding, bHoming, fHomingSpeed);
        }
        else
        {
            pBulletMain.Init(pBulletObject, pTransform, vSpawnPosition, vScale, enBulletType, enEnemyBulletType, fAlpha, fPadding, bHoming, fHomingSpeed);
        }

        if (pBulletMain.GetBulletBase().GetBulletType() != EBulletType.enType_Empty)
        {
            SpriteRenderer pSpriteRenderer = pTransform.GetChild(0).GetComponent<SpriteRenderer>();
            pBulletMain.GetBulletBase().SetSpriteRenderer(pSpriteRenderer);
            pSpriteRenderer.color = new Color(1, 1, 1, fAlpha);
        }    
        return pBulletObject;
    }
    #endregion
}

public class BulletManager : Singleton<BulletManager>
{
    #region VARIABLE
    private BulletPool pBulletPool;
    #endregion

    #region GET METHOD
    public BulletPool GetBulletPool() { return pBulletPool; }
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        Transform pBulletParent = GameObject.Find("BulletPool").GetComponent<Transform>();
        Transform pActiveBulletParent = GameObject.Find("ActiveBullets").GetComponent<Transform>();
        Transform pActivePlayerBulletParent = GameObject.Find("ActivePlayerBullets").GetComponent<Transform>();
        pBulletPool = new BulletPool(pBulletParent, pActiveBulletParent, pActivePlayerBulletParent);
    }
    #endregion
}