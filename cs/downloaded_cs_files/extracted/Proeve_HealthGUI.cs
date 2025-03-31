using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PeppaSquad.UI {
    public class HealthGUI : MonoBehaviour {
        [SerializeField]
        private Image healthBar;

        [SerializeField]
        private float speed = 1;

        [SerializeField]
        private float lerpPrecision = 0.001f;

        private int maxHealth = 100;

        private float currentGoal;
        private Coroutine healthCoroutine;

        /// <summary>
        /// Changes the health timer
        /// </summary>
        /// <param name="health"></param>
        public void ChangeHealth(int health) {
            currentGoal = (float) health / maxHealth;
            if (healthCoroutine == null) healthCoroutine = StartCoroutine(HealthUpdate());
        }

        /// <summary>
        /// Change the max health
        /// </summary>
        /// <param name="health">The new max health</param>
        public void SetMaxHealth(int health) {
            maxHealth = health;
            ChangeHealth(health);
        }

        private void OnDisable() {
            if (healthCoroutine != null) {
                StopCoroutine(healthCoroutine);
                healthCoroutine = null;
            }
        }

        /// <summary>
        /// Lerps the health bar
        /// </summary>
        /// <returns></returns>
        private IEnumerator HealthUpdate() {
            while ((healthBar.fillAmount - currentGoal) > lerpPrecision) {
                healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentGoal, Time.deltaTime * speed);
                yield return null;
            }
            healthBar.fillAmount = currentGoal;
            healthCoroutine = null;
        }
    }
}