#region USING
using UnityEngine;
#endregion

public class EffectBase : GameObjectBase, IObjectBase
{
    #region VARIABLE
    private SpriteRenderer pSpriteRenderer;
    private Animator pAnimator;
    private Color pColor;
    private EEffectType enEffectType;
    private EEffectAnimationType enEffectAnimationType;
    private float fEffectRotateAngle;
    private float fEffectRotateAngleAccumulation;
    private float fEffectParentRotateSpeed;
    private float fEffectSpriteRotateSpeed;
    private float fEffectScaleUpSpeed;
    private float fEffectScaleDownSpeed;
    private float fEffectScalePingpongLimit;
    private float fEffectAlphaUpSpeed;
    private float fEffectAlphaDownSpeed;
    private bool bCondition;
    private bool bEffectScalePingpongFlag;
    #endregion

    #region CONSTRUCTOR
    public EffectBase(GameObject pEffectObject, Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale)
    {
        base.Init(pEffectObject, pTransform, vSpawnPosition, vScale, EGameObjectType.enType_Effect);
    }
    #endregion

    #region GET METHOD
    public SpriteRenderer GetSpriteRenderer() { return pSpriteRenderer; }
    public Animator GetAnimator() { return pAnimator; }
    public Color GetColor() { return pColor; }
    public EEffectType GetEffectType() { return enEffectType; }
    public EEffectAnimationType GetEffectAnimationType() { return enEffectAnimationType; }
    public float GetEffectRotateAngle() { return fEffectRotateAngle; }
    public float GetEffectRotateAngleAccumulation() { return fEffectRotateAngleAccumulation; }
    public float GetEffectParentRotateSpeed() { return fEffectParentRotateSpeed; }
    public float GetEffectSpriteRotateSpeed() { return fEffectSpriteRotateSpeed; }
    public float GetEffectScaleUpSpeed() { return fEffectScaleUpSpeed; }
    public float GetEffectScaleDownSpeed() { return fEffectScaleDownSpeed; }
    public float GetEffectScalePingpongLimit() { return fEffectScalePingpongLimit; }
    public float GetEffectAlphaUpSpeed() { return fEffectAlphaUpSpeed; }
    public float GetEffectAlphaDownSpeed() { return fEffectAlphaDownSpeed; }
    public bool GetCondition() { return bCondition; }
    public bool GetEffectScalePingpongFlag() { return bEffectScalePingpongFlag; }
    #endregion

    #region SET METHOD
    public void SetSpriteRenderer(SpriteRenderer pSpriteRenderer) { this.pSpriteRenderer = pSpriteRenderer; }
    public void SetAnimator(Animator pAnimator) { this.pAnimator = pAnimator; }
    public void SetColor(Color pColor) { this.pColor = pColor; }
    public void SetEffectType(EEffectType enEffectType) { this.enEffectType = enEffectType; }
    public void SetEffectAnimationType(EEffectAnimationType enEffectAnimationType) { this.enEffectAnimationType = enEffectAnimationType; }
    public void SetEffectRotateAngle(float fEffectRotateAngle)
    {
        this.fEffectRotateAngle = fEffectRotateAngle;
        SetRotationZ(fEffectRotateAngle);
    }
    public void SetEffectRotateAngleAccumulation(float fEffectRotateAngleAccumulation) { this.fEffectRotateAngleAccumulation = fEffectRotateAngleAccumulation; }
    public void SetEffectParentRotateSpeed(float fEffectParentRotateSpeed) { this.fEffectParentRotateSpeed = fEffectParentRotateSpeed; }
    public void SetEffectSpriteRotateSpeed(float fEffectSpriteRotateSpeed) { this.fEffectSpriteRotateSpeed = fEffectSpriteRotateSpeed; }
    public void SetEffectScaleUpSpeed(float fEffectScaleUpSpeed) { this.fEffectScaleUpSpeed = fEffectScaleUpSpeed; }
    public void SetEffectScaleDownSpeed(float fEffectScaleDownSpeed) { this.fEffectScaleDownSpeed = fEffectScaleDownSpeed; }
    public void SetEffectScalePingpongLimit(float fEffectScalePingpongLimit) { this.fEffectScalePingpongLimit = fEffectScalePingpongLimit; }
    public void SetEffectAlphaUpSpeed(float fEffectAlphaUpSpeed) { this.fEffectAlphaUpSpeed = fEffectAlphaUpSpeed; }
    public void SetEffectAlphaDownSpeed(float fEffectAlphaDownSpeed) { this.fEffectAlphaDownSpeed = fEffectAlphaDownSpeed; }
    public void SetEffect(float fEffectRotateAngle, float fEffectParentRotateSpeed, float fEffectSpriteRotateSpeed, float fEffectScaleUpSpeed, float fEffectScaleDownSpeed, float fEffectAlphaUpSpeed, float fEffectAlphaDownSpeed, float fEffectScalePingpongLimit = 0.0f)
    {
        SetEffectRotateAngle(fEffectRotateAngle);
        SetEffectRotateAngleAccumulation(0.0f);
        SetEffectParentRotateSpeed(fEffectParentRotateSpeed);
        SetEffectSpriteRotateSpeed(fEffectSpriteRotateSpeed);
        SetEffectScaleUpSpeed(fEffectScaleUpSpeed);
        SetEffectScaleDownSpeed(fEffectScaleDownSpeed);
        SetEffectScalePingpongLimit(fEffectScalePingpongLimit);
        SetEffectAlphaUpSpeed(fEffectAlphaUpSpeed);
        SetEffectAlphaDownSpeed(fEffectAlphaDownSpeed);
    }
    public void SetCondition(bool bCondition) { this.bCondition = bCondition; }
    public void SetEffectScalePingpongFlag(bool bEffectScalePingpongFlag) { this.bEffectScalePingpongFlag = bEffectScalePingpongFlag; }
    #endregion

    #region COMMON METHOD
    public override void AllReset()
    {
        SetColor(new Color(1, 1, 1, 0));
        SetEffectType(EEffectType.None);
        SetEffectAnimationType(EEffectAnimationType.None);
        SetEffect(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
        SetCondition(false);
        SetEffectScalePingpongFlag(false);

        base.AllReset();
    }
    #endregion
}
