using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;
using UnityEngine.UI;
namespace TK
{
    public class UI_StatBar : MonoBehaviour
    {
        private Slider slider;
        // VARIABLE TO SCALE BAR SIZE DEPENDING ON STAT (HIGHEST STAT = LONGER BAR ACROSS SCREEN)
        // SECONDARY BAR BEHIND MAY BAR FOR POLISH EFFECT (YELLOW BAR THAT SHOWS HOW MUCH AN ACTION/ DAMAGE TAKES AWAY FORM CUREENT STAT)

        protected virtual void Awake()
        {
            slider= GetComponent<Slider>();
        }

        public virtual void SetStat(int newValue)
        {
            slider.value = newValue;
        }
        public virtual void SetMaxStat(int maxValue)
        {
            slider.maxValue = maxValue;
            slider.value = maxValue;
        }
    }
}
