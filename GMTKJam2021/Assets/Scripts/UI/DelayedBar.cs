using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK2021.UI
{
    public class DelayedBar : MonoBehaviour
    {
        [SerializeField]
        private float maxValue;

        [SerializeField]
        private float currentValue;

        [SerializeField]
        private float delayedValue;

        [SerializeField]
        private float maximumValue;

        [SerializeField]
        private Image.OriginHorizontal direction;

        [SerializeField]
        private Gradient color;

        [SerializeField]
        private Gradient delayColor;

        [SerializeField]
        private Image foregroundBar;

        [SerializeField]
        private Image delayedBar;

        [SerializeField]
        private Image MaximumBar;

        private bool sleeping;


        [SerializeField]
        private AnimationCurve transition;

        public void SetValue(float value) => SetValue(value, false);

        public void SetValue(float value, bool setImmediately)
        {
            value = Mathf.Clamp(value, 0, maxValue);
            if (setImmediately)
            {
                delayedValue = value;
            }
            currentValue = value;

            if (sleeping)
            {
                StartCoroutine(AnimateDelay());
            }
        }

        public void SetMaxValue(float value) => maximumValue = value;

        public IEnumerator AnimateDelay()
        {
            sleeping = false;
            float startValue = delayedValue;
            bool wasIncreasing = delayedValue < currentValue;
            while (!Mathf.Approximately(currentValue, delayedValue))
            {
                float transitionValue = transition.Evaluate(GetPercentage(startValue, currentValue, delayedValue));

                if (delayedValue < currentValue)
                {
                    delayedValue += Mathf.Min(Mathf.Abs(currentValue - delayedValue), transitionValue);
                    if (!wasIncreasing)
                    {
                        startValue = currentValue;
                        wasIncreasing = true;
                    }
                }
                else
                {
                    delayedValue -= Mathf.Min(Mathf.Abs(currentValue - delayedValue), transitionValue);
                    if (wasIncreasing)
                    {
                        startValue = currentValue;
                        wasIncreasing = false;
                    }
                }
                RefreshBars();
                yield return null;
            }

            delayedValue = currentValue;
            sleeping = true;
            RefreshBars();
        }

        private void Start()
        {
            delayedBar.fillOrigin = (int)direction;
            foregroundBar.fillOrigin = (int)direction;
            //MaximumBar.fillOrigin = 1 - (int)direction;
            RefreshBars();
            sleeping = true;
        }

        private void RefreshBars()
        {
            if (maxValue == 0)
                return;

            float relativeCurrentValue = currentValue / maxValue;
            float relativeDelayedValue = delayedValue / maxValue;
            float relativeMaxedValue = maximumValue / maxValue;
            SetMaxBarValues(relativeMaxedValue);
            if (delayedValue > currentValue)
            {
                SetForegroundBarValues(relativeCurrentValue);
                SetDelayedBarValues(relativeDelayedValue);
            }
            else
            {
                SetForegroundBarValues(relativeDelayedValue);
                SetDelayedBarValues(relativeCurrentValue);
            }
            
        }

        private void SetForegroundBarValues(float value)
        {
            value = Mathf.Clamp01(value);
            foregroundBar.fillAmount = value;
            foregroundBar.color = color.Evaluate(value);
        }

        private void SetDelayedBarValues(float value)
        {
            value = Mathf.Clamp01(value);
            delayedBar.fillAmount = value;
            delayedBar.color = delayColor.Evaluate(value);
        }

        private void SetMaxBarValues(float value)
        {
            value = Mathf.Clamp01(value);
            RectTransform rectTransform = MaximumBar.transform as RectTransform;
            rectTransform.anchoredPosition = 
                new Vector2(direction == Image.OriginHorizontal.Left ? value * rectTransform.sizeDelta.x : - value * rectTransform.sizeDelta.x, rectTransform.anchoredPosition.y);
            //MaximumBar.fillAmount = 1 - value;
        }

        private float GetPercentage(float a, float b, float x)
        {
            if (a == b)
                return 1;

            return (x - b) / (a - b);
        }
    }
}
