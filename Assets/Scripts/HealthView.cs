
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    public Image[] potions;
    public Sprite fullPotion;
    public Sprite emptyPotion;

    void Start()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        for (int i = 0; i < potions.Length; i++)
        {
            if (i < PlayerData.currentHealth)
            {
                potions[i].sprite = fullPotion;
            }
            else
            {
                potions[i].sprite = emptyPotion;
            }
        }
    }
}
