using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 3;
    [SerializeField]
    private int curHp = 3;
    [SerializeField]
    private MeshRenderer playerModeling;
    [SerializeField]
    private TMP_Text hpText;
    [SerializeField]
    private Ease ease;
    private Material playerMaterial;
    private SphereCollider playerCollider;
    private bool invincibility = false;
    private Sequence blinkSequence;

    private void Start()
    {
        playerMaterial = playerModeling.material;
        playerCollider = gameObject.GetComponent<SphereCollider>();
        ReSetHp();
        ChangeText(maxHp);
    }
    private void Die()
    {
        LifeManager.Instance.SubtractHp(1);
    }
    public void ReSetHp()
    {
        curHp = maxHp;
    }
    private void Hit()
    {
        if (invincibility) return;
        curHp--;
        ChangeText(curHp);
        Blinking();
        if (curHp <= 0)
        {
            Die();
        }
    }
    private void Blinking()
    {
        blinkSequence = DOTween.Sequence()
            .SetAutoKill(false)
            .OnStart(() =>
            {
                invincibility = true;
                playerCollider.enabled = false;
            })
            .Append(playerMaterial.DOColor(Color.clear, 0.3f))
            .Append(playerMaterial.DOColor(Color.white, 0.4f))
            .OnComplete(() =>
            {
                playerCollider.enabled = true;
                invincibility = false;
                print("end blink");
            }).SetLoops(3).SetEase(ease).SetDelay(0.4f);
    }
    private void ChangeText(int hp)
    {
        hpText.text = $"{hp} HP";
        if(hp==1)
            hpText.color = Color.red;
        else
            hpText.color = Color.green;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Hit();
            Blinking();
        }
    }
}
