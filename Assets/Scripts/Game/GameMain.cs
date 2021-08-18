#region USING
using UnityEngine;
#endregion

public class GameMain : MonoBehaviour
{
    #region VARIABLE
    #endregion

    #region UNITY LIFE CYCLE
    public void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void Start()
    {
        GameManager.Instance.Init();
        BulletManager.Instance.Init();
        EffectManager.Instance.Init();
        EnemyManager.Instance.Init();
    }
    #endregion
}