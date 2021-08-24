using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : GameObjectBase, IObjectBase
{
	#region VARIABLE
	private SpriteRenderer pSpriteRenderer;
	private Animator pAnimator;
	private Rigidbody2D pRigidbody;
	private CircleCollider2D pCircleCollider;
	private Color pColor;
	private EItemType enItemType;
	private float fPadding;
	private bool bAutoCollect;
	private bool bSpriteRotate;
    #endregion

    #region CONSTRUCTOR
    public ItemBase(GameObject pItemObject, Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale)
    {
        base.Init(pItemObject, pTransform, vSpawnPosition, vScale, EGameObjectType.enType_Item);
    }
    #endregion

    #region GET METHOD
    public SpriteRenderer GetSpriteRenderer() { return pSpriteRenderer; }
    public Animator GetAnimator() { return pAnimator; }
    public Rigidbody2D GetRigidbody() { return pRigidbody; }
    public CircleCollider2D GetCircleCollider() { return pCircleCollider; }
    public Color GetColor() { return pColor; }
    public EItemType GetItemType() { return enItemType; }
    public float GetPadding() { return fPadding; }
    public bool GetAutoCollect() { return bAutoCollect; }
    public bool GetSpriteRotate() { return bSpriteRotate; }
    #endregion

    #region SET METHOD
    public void SetSpriteRenderer(SpriteRenderer pSpriteRenderer) { this.pSpriteRenderer = pSpriteRenderer; }
    public void SetAnimator(Animator pAnimator) { this.pAnimator = pAnimator; }
    public void SetRigidbody(Rigidbody2D pRigidbody) { this.pRigidbody = pRigidbody; }
    public void SetCircleCollider(CircleCollider2D pCircleCollider) { this.pCircleCollider = pCircleCollider; }
    public void SetColor(Color pColor) { this.pColor = pColor; }
    public void SetItemType(EItemType enItemType) { this.enItemType = enItemType; }
    public void SetPadding(float fPadding) { this.fPadding = fPadding; }
    public void SetAutoCollect(bool bAutoCollect) { this.bAutoCollect = bAutoCollect; }
    public void SetSpriteRotate(bool bSpriteRotate) { this.bSpriteRotate = bSpriteRotate; }
    #endregion

    #region COMMON METHOD
    public override void AllReset()
    {
        SetColor(Color.white);
        SetItemType(EItemType.None);
        SetPadding(0.0f);
        SetAutoCollect(false);
        SetSpriteRotate(false);

        base.AllReset();
    }
    #endregion
}
