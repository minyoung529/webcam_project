using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface HpSubEvent
{
    void Active(int hp);
}
public class LifeManager : MonoSingleton<LifeManager>
{
    [SerializeField]
    private int totalHP = 3;

    private List<HpSubEvent> subEvents =new();

    public void AddEvent(HpSubEvent subEvent)
    {
        subEvents.Add(subEvent);
    }
    public int GetHP() => totalHP;
    public void SubtractHp(int subhp)
    {
        totalHP -= subhp;
        if(totalHP <= 0)
        {
            ShootingManager.Instance.GameOver();
            return;
        }
        subEvents.All((x) => { x.Active(totalHP); return true; });
    }


}
