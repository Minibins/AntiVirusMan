using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DustyStudios
{
    public class TypewriterEffect : MonoBehaviour
    {
        [SerializeField] private float _delay = 0.1f;
        private Text _text;
        private string _fullText;
        private string _currentText = "";

        private void Start()
        {
            _text = GetComponent<Text>();
            _fullText = _text.text;
            _text.text = "";
            StartCoroutine(ShowText());
        }

        IEnumerator ShowText()
        {
            for (int i = 0; i <= _fullText.Length; i++)
            {
                _currentText = "[" + _fullText.Substring(0, i) + "]";
                _text.text = _currentText;
                yield return new WaitForSeconds(_delay);
            }
        }
    }
}