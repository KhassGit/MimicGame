using System;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class UIController: MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI healthLabel;
        [SerializeField] public GameObject healthBar;

        public void SetHealth(float health)
        {
            healthLabel.text = health.ToString("0");
            SetHealthBarWidth(health);
        }

        private void SetHealthBarWidth(float health)
        {
            var width = Math.Clamp(400 * health / 100, 0, 400);
            healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 40);
        }
    }
}