using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DustyStudios
{
    public class TimeAnimation : MonoBehaviour
    {
        [SerializeField] private float duration = 10f;
        
        private Text uiText;
        
        public string endTime = "";

        private int startMinutes = 0;
        private int startSeconds = 0;
        private int endMinutes;
        private int endSeconds;
        private float elapsedTime = 0f;

        private void Start()
        {
            uiText = GetComponent<Text>();
            string[] timeParts = endTime.Split(':');
            endMinutes = int.Parse(timeParts[0]);
            endSeconds = int.Parse(timeParts[1]);
            StartCoroutine(AnimateTime());
        }

        IEnumerator AnimateTime()
        {
            float startTotalSeconds = startMinutes * 60 + startSeconds;
            float endTotalSeconds = endMinutes * 60 + endSeconds;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                
                float currentTotalSeconds = Mathf.Lerp(startTotalSeconds, endTotalSeconds, t);
                int minutes = Mathf.FloorToInt(currentTotalSeconds / 60);
                int seconds = Mathf.FloorToInt(currentTotalSeconds % 60);

                uiText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);

                yield return null;
            }
            uiText.text = string.Format("{0:D2}:{1:D2}", endMinutes, endSeconds);
        }
    }
}