using System;
public enum EnemyState
{
    None,
    Idle,
    Moving,
}


[Serializable]
public class EnemyStat
{
    public int hp = 1;
    public float speed = 5f;
    public EnemyState state = EnemyState.Idle;
}
