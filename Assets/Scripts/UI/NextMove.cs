using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextMove : MonoBehaviour
{
    public Sprite EnrageIcon;
    public Sprite ArmorIcon;
    public Sprite AttackIcon;

    public SpriteRenderer spriteRenderer;
    public TextMeshProUGUI Text;

    public void UpdateUI(int enemyAction, int damage)
    {
        Text.enabled = false;
        if (enemyAction == 0) {
            spriteRenderer.sprite = AttackIcon;
            Text.enabled = true;
            Text.text = damage.ToString();
        } else if (enemyAction == 1) {
            spriteRenderer.sprite = ArmorIcon;
        } else if (enemyAction == 2) {
            spriteRenderer.sprite = EnrageIcon;
        }
    }
}
