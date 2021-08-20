#region USING
using UnityEngine;
#endregion

public interface IObjectBase
{
    SpriteRenderer GetSpriteRenderer();
    Animator GetAnimator();
    Color GetColor();

    void SetSpriteRenderer(SpriteRenderer pSpriteRenderer);
    void SetAnimator(Animator pAnimator);
    void SetColor(Color pColor);

    void AllReset();
}

public interface IPoolBase
{
    GameObject AddPool();
    void ReturnPool(GameObject pObject);
}