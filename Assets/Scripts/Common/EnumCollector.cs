#region GAMEOBJECT
public enum EGameObjectType
{
    None = -1,
    enType_Player,
    enType_Enemy,
    enType_Boss,
    enType_Bullet,
    enType_Effect,
    enType_Item,
    enType_Area,
    enType_UI,
    Max
}
#endregion

#region PLAYER
public enum EPlayerType
{
    None = -1,
    enType_Reimu,
    enType_Marisa,
    Max
}
public enum EPlayerWeaponType
{
    None = -1,
    enType_A,
    enType_B,
    Max
}
#endregion

#region ENEMY
public enum EEnemyType
{
    None = -1,
    enType_TinyFairy_Type1 = 0,
    enType_TinyFairy_Type2 = 12,
    enType_TinyFairy_Type3 = 24,
    enType_TinyFairy_Type4 = 36,
    enType_TinyFairy_Type5 = 48,
    enType_TinyFairy_Type6 = 60,
    enType_TinyFairy_Type7 = 72,
    enType_TinyFairy_Type8 = 84,
    enType_TinyFairy_Type9 = 96,
    enType_TinyFairy_Type10 = 108,
    enType_TinyFairy_Type11 = 120,
    enType_TinyFairy_Type12 = 132,
    enType_TinyFairy_Type13 = 144,
    enType_TinyFairy_Type14 = 156,
    enType_TinyFairy_Type15 = 168,
    enType_TinyFairy_Type16 = 180,
    enType_TinyFairy_Type17 = 192,
    enType_TinyFairy_Type18 = 204,
    enType_TinyFairy_Type19 = 216,
    enType_TinyFairy_Type20 = 228,
    enType_TinyFairy_Type21 = 240,
    enType_TinyFairy_Type22 = 252,
    enType_HugeFairy_Type1 = 264,
    enType_HugeFairy_Type2 = 276,
    enType_HugeFairy_Type3 = 288,
    enType_HugeFairy_Type4 = 300,
    enType_HugeFairy_Type5 = 312,
    enType_HugeFairy_Type6 = 324,
    enType_HugeFairy_Type7 = 336,
    enType_HugeFairy_Type8 = 348,
    enType_HugeFairy_Type9 = 360,
    enType_HugeFairy_Type10 = 372,
    enType_Ghost_Type1 = 384,
    enType_Ghost_Type2 = 392,
    enType_Ghost_Type3 = 400,
    enType_Ghost_Type4 = 408,
    enType_Yinyang_Large = 416,
    enType_Yinyang_Small = 420,
    enType_MagicCircle_Type1 = 424,
    enType_MagicCircle_Type2 = 428,
    enType_Ghost_Type5 = 432,
    enType_MagicCircle_Type3 = 440,
    Max = 441
}
#endregion

#region BULLET
public enum EBulletType
{
    None = -1,
    enType_Empty,
    enType_Parent,
    enType_Box,
    enType_Capsule,
    enType_Circle,
    Max
}
public enum EBulletShooter
{
    None = -1,
    enShooter_Player,
    enShooter_Enemy,
    Max
}
public enum EPlayerBulletType
{
    None = -1,
    enType_ReimuPrimary = 47,
    enType_ReimuSecondary_Niddle = 50,
    enType_ReimuSecondary_Homing,
    enType_MarisaPrimary,
    enType_MarisaSecondary_Missile,
    enType_MarisaSecondary_Laser,
    Max
}
public enum EEnemyBulletType
{
    None = -1,
    enType_BoxLaser = 0,
    enType_Arrowhead = 16,
    enType_DoubleCircle = 32,
    enType_Circle = 48,
    enType_Capsule = 64,
    enType_Kunai = 80,
    enType_JewelCapsule = 96,
    enType_Amulet = 112,
    enType_Bullet = 128,
    enType_VoidCapsule = 144,
    enType_TinyStar = 160,
    enType_TinyVoidCircle = 192,
    enType_TinyCapsule = 208,
    enType_Coin = 224,
    enType_TinyCircle = 235,
    enType_Star = 261,
    enType_HugeCircle = 269,
    enType_Butterfly = 277,
    enType_Knife = 285,
    enType_HugeCapsule = 293,
    enType_BubbleCircle = 309,
    enType_Heart = 313,
    enType_Arrow = 321,
    enType_GhostCircle = 337,
    enType_TinyArrowhead = 353,
    enType_MagicCircle = 369,
    enType_LightCircle = 377,
    enType_MusicNoteTone = 385,
    enType_MusicNoteRest = 397,
    enType_CurvedLaser = 405,
    enType_LightningLaser = 421,
    Max = 425
}
#endregion

#region EFFECT
public enum EEffectType
{
    None = -1,
    enType_RotateCircleEffect = 176,
    enType_CommonEffect = 227,
    enType_CircleEffect = 251,
    enType_DestroyEffect = 256,
    enType_DummyEffect = 301,
    Max = 309
}
public enum EEffectAnimationType
{
    None = -1,
    enType_Common,
    enType_BulletShot,
    enType_HoldingLaserShot,
    enType_MovingLaserShot,
    enType_MovingCurvedLaserShot,
    enType_Animation,
    enType_Explosion,
    Max
}
#endregion

#region UI
#endregion

#region SYSTEM
public enum EGameDifficulty
{
    None = -1,
    enDifficulty_Easy,
    enDifficulty_Normal,
    enDifficulty_Hard,
    enDifficulty_Lunatic,
    enDifficulty_Extra,
    Max
}
#endregion