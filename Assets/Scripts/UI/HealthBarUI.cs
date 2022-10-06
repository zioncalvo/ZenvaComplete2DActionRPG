using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image healthBarFill;

    void OnEnable()
    {
        //enabling the updateHealthBar function to listen to the event/subscribe to the event
        character.onTakeDamage += updateHealthBar;
        character.onHeal += updateHealthBar;
    }

    void OnDisable()
    {
        character.onTakeDamage -= updateHealthBar;
        character.onHeal -= updateHealthBar;
    }

    void Start()
    {
        SetNameText(character.DisplayName);
    }

    void SetNameText (string text)
    {
        nameText.text = text;
    }

    void updateHealthBar()
    {
        float healthPercent = (float)character.CurHP/(float)character.MaxHP;
        healthBarFill.fillAmount = healthPercent;
    }
}
