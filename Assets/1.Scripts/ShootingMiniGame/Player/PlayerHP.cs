using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 3;
    [SerializeField]
    private int curHp = 3;
    [SerializeField]
    private MeshRenderer playerModeling;
    [SerializeField]
    private Ease ease;
    private Material playerMaterial;
    private SphereCollider playerCollider;
    private bool invincibility = false;
    private void Start()
    {
        playerMaterial = playerModeling.material;
        playerCollider = gameObject.GetComponent<SphereCollider>();
        ReSetHp();
        Blinking();
    }
    private void Die()
    {

    }
    private void Hit()
    {
        if (invincibility) return;
        curHp--;
        if (curHp <= 0)
        {
            Die();
        }
    }
    private void Blinking()
    {
        invincibility = true;
        playerCollider.enabled = false;
        playerMaterial.DOColor(Color.clear, 0.2f).OnStepComplete(() =>
        {  playerMaterial.DOColor(Color.clear, 0.2f);}).OnComplete(() => {
            playerCollider.enabled = true;
            invincibility = false;
        }).SetLoops(5).SetEase(ease);
        
    }
    public void ReSetHp()
    {
        curHp = maxHp;
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
