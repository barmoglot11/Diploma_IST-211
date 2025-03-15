using System;
using DIARY;
using QUEST;
using UnityEngine;

namespace _TEST_
{
    public class QuestTest : MonoBehaviour
    {
        public DiaryInterface diaryInterface;
        private void Start()
        {
            QuestManager.Instance.StartQuest("mc1");
            diaryInterface.SetUI();
        }
    }
}