#region USING
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public delegate void DelegateCommon();
public delegate void DelegateGameObject(GameObject pObject);
public delegate IEnumerator<float> DelegateEnemyMoveRepeat(GameObject pObject);
public delegate void DelegateItemBase(ItemBase pItemBase);