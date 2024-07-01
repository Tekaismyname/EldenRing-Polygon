using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TK
{
    public class PlayerUIPopUpManager : MonoBehaviour
    {
        [Header("YOU DIE Pop Up")]
        [SerializeField] GameObject youDiePopUpGameObject;
        [SerializeField] TextMeshProUGUI youDiePopUpBackgroundText;
        [SerializeField] TextMeshProUGUI youDiePopUpText;
        [SerializeField] CanvasGroup youDiePopUpCanvasGroup; // allow to set alpha to fade over time

        public void SendYouDiePopUp()
        {
            // ACTIVE POST PROCESSING EFFECTS

            youDiePopUpGameObject.SetActive(true);
            youDiePopUpBackgroundText.characterSpacing = 0;
            
            StartCoroutine(StretchPopUpTextOverTime(youDiePopUpBackgroundText, 8, 19));
            StartCoroutine(FadeInPopUpOverTime(youDiePopUpCanvasGroup, 5));
            StartCoroutine(WaitThenFadeOutPopUpOverTime(youDiePopUpCanvasGroup,2 ,5));

        }

        private IEnumerator StretchPopUpTextOverTime(TextMeshProUGUI text, float duration, float stretchAmount)
        {
            if(duration >0f)
            {
                text.characterSpacing = 0;
                float timer = 0;

                yield return null;

                while(timer < duration)
                {
                    timer += Time.deltaTime;
                    text.characterSpacing = Mathf.Lerp(text.characterSpacing, stretchAmount, duration * (Time.deltaTime / 20));
                    yield return null;
                }
            }
        }

        private IEnumerator FadeInPopUpOverTime(CanvasGroup canvas, float duration)
        {
            if (duration > 0f)
            {
                canvas.alpha = 0f;
                float timer = 0;

                yield return null;

                while (timer < duration)
                {
                    timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 1, duration*Time.deltaTime);
                    yield return null;
                }
            }

            canvas.alpha = 1;

            yield return null;
        }

        private IEnumerator WaitThenFadeOutPopUpOverTime(CanvasGroup canvas, float duration, float delay)
        {
            if (duration > 0f)
            {
                while(delay > 0)
                {
                    delay -= Time.deltaTime;
                    yield return null;
                }

                canvas.alpha = 1;
                float timer = 0;

                yield return null;

                while (timer < duration)
                {
                    timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 0, duration * Time.deltaTime);
                    yield return null;
                }
            }

            canvas.alpha = 0;

            yield return null;
        }
    }

}