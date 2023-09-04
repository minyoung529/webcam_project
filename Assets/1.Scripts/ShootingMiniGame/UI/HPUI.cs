using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour, HpSubEvent
{
    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private GameObject imagePrefab;
    [SerializeField]
    private Color disabledColor = Color.gray;
    private List<Image> hpImage = new List<Image>();
    private int maxHp = 3;
    private int hp = 0;
    private void Start()
    {
        LifeManager.Instance.AddEvent(this);
        maxHp = LifeManager.Instance.GetHP();
        InitImage(maxHp);
        ChangeUI(maxHp);
    }
    public void Active(int hp)
    {
        ChangeUI(hp);
    }
    private void InitImage(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Image image = Instantiate(imagePrefab, content).GetComponent<Image>();
            hpImage.Add(image);
        }
    }
    private void DisableImage(int idx)
    {
        hpImage[idx].color = disabledColor;
    }
    private void EnableImage(int idx)
    {
        hpImage[idx].color = Color.white;
    }
    private void ChangeUI(int hp)
    {
        this.hp = hp;
        text.text = $"{hp} LEFT";
        for (int i = 0; i < maxHp; i++)
        {
            if (i < hp)
                EnableImage(i);
            else
                DisableImage(i);
        }
    }
}
