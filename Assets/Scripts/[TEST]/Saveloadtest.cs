using System;
using System.Collections.Generic;
using QUEST;
using SAVELOAD;
using UnityEngine;

namespace _TEST_
{
    public class Saveloadtest : MonoBehaviour
    {
        private void Start()
        {
            Quest quest1 = new Quest();
            quest1.QuestID = "mq101";
            quest1.QuestName = "mq101";
            quest1.QuestDescription = new Dictionary<int, string>()
            {
                {1, "dfafa"},
                {2, "fasfas"},
                {3, "gfdhfdg"}
            };
            quest1.QuestStage = 1;
            
            var sljson = new SaveLoadJsonService();
            sljson.Save("quest1", quest1);
            Quest quest2;
            sljson.Load("quest1",out quest2);
            Debug.Log(quest2.QuestID);
            
            var slbin = new SaveLoadBinService();
            slbin.Save("quest4", quest1);
            Quest quest3;
            slbin.Load("quest4",out quest3);
            Debug.Log(quest3.QuestStage);
        }
    }
}