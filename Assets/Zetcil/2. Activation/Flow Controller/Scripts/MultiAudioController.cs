using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{

    public class MultiAudioController : MonoBehaviour
    {

        [Header("Main Settings")]
        public AudioSource TargetAudio;

        [Header("Language Settings")]
        public VarLanguage TargetLanguage;
        public AudioClip IndonesianClip;
        public AudioClip EnglishClip;
        public AudioClip ArabicClip;
        public AudioClip ChineseClip;
        public AudioClip KoreanClip;
        public AudioClip JapaneseClip;

        bool isStart = false;

        void InitAudio()
        {
            if (TargetLanguage.LanguageType == VarLanguage.CLanguageType.Indonesian)
            {
                TargetAudio.clip = IndonesianClip;
            }
            if (TargetLanguage.LanguageType == VarLanguage.CLanguageType.English)
            {
                TargetAudio.clip = EnglishClip;
            }
            if (TargetLanguage.LanguageType == VarLanguage.CLanguageType.Arabic)
            {
                TargetAudio.clip = ArabicClip;
            }
            if (TargetLanguage.LanguageType == VarLanguage.CLanguageType.Chinese)
            {
                TargetAudio.clip = ChineseClip;
            }
            if (TargetLanguage.LanguageType == VarLanguage.CLanguageType.Korean)
            {
                TargetAudio.clip = KoreanClip;
            }
            if (TargetLanguage.LanguageType == VarLanguage.CLanguageType.Japanese)
            {
                TargetAudio.clip = JapaneseClip;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (!isStart)
            {
                InitAudio();
                isStart = true;
            }
        }
    }
}
