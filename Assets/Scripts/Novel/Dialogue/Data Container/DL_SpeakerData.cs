﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DIALOGUE
{
    public class DL_SpeakerData
    {
        public string name, castName;
        public string displayName => (castName != string.Empty ? castName : name);
        public Vector2 castPosition;
        public List<(int layer, string expresion)> CastExpressions { get; set; }
        public bool makeCharacterEnter = false;

        public bool isCastingName =>castName != string.Empty;
        public bool isCastingPosition = false;
        public bool isCastingExpressions => CastExpressions.Count > 0;

        private const string NAMECAST_ID = " as ";
        private const string POSITIONCAST_ID = " at ";
        private const string EXPRESSIONCAST_ID = " [";
        private const char AXISDELIMITER = ':';
        private const char EPRESSIONLAYER_JOINER = ',';
        private const char EXPRESSIONLAYER_DELIMITER = ':';

        private const string ENTER_KEYWORD = "enter";

        private string ProcessKeyword(string rawSpeaker)
        {
            if (rawSpeaker.StartsWith(ENTER_KEYWORD))
            {
                rawSpeaker = rawSpeaker.Substring(ENTER_KEYWORD.Length);
                makeCharacterEnter = true;
            }

            return rawSpeaker;
        }

        public DL_SpeakerData(string rawSpeaker)
        {
            rawSpeaker = ProcessKeyword(rawSpeaker);

            string pattern = @$"{NAMECAST_ID}|{POSITIONCAST_ID}|{EXPRESSIONCAST_ID.Insert(EXPRESSIONCAST_ID.Length-1,@"\")}";
            MatchCollection matches = Regex.Matches(rawSpeaker, pattern);

            castName = "";
            castPosition = Vector2.zero;
            CastExpressions = new List<(int layer, string expresion)>();

            if(matches.Count == 0)
            {
                name = rawSpeaker;
                return;
            }

            int index = matches[0].Index;

            name = rawSpeaker.Substring(0, index);

            for(int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];
                int startIndex = 0, endIndex = 0;


                if(match.Value == NAMECAST_ID)
                {
                    startIndex = match.Index + NAMECAST_ID.Length;
                    endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;

                    castName = rawSpeaker.Substring(startIndex, endIndex - startIndex);
                }
                else if(match.Value == POSITIONCAST_ID)
                {
                    isCastingPosition = true;

                    startIndex = match.Index + POSITIONCAST_ID.Length;
                    endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;

                    string castPos = rawSpeaker.Substring(startIndex, endIndex - startIndex);

                    string[] axis = castPos.Split(AXISDELIMITER, System.StringSplitOptions.RemoveEmptyEntries);

                    float.TryParse(axis[0], out castPosition.x);
                    if(axis.Length > 1)
                        float.TryParse(axis[1], out castPosition.y);
                }
                else if(match.Value == EXPRESSIONCAST_ID)
                {
                    startIndex = match.Index + EXPRESSIONCAST_ID.Length;
                    endIndex = (i < matches.Count - 1) ? matches[i + 1].Index : rawSpeaker.Length;

                    string castExp = rawSpeaker.Substring(startIndex, endIndex - (startIndex+1));

                    CastExpressions = castExp.Split(EPRESSIONLAYER_JOINER)
                        .Select(x =>
                        {
                            var parts = x.Trim().Split(EXPRESSIONLAYER_DELIMITER);
                            if (parts.Length == 2)
                                return (int.Parse(parts[0]), parts[1]);
                            else
                                return (0, parts[0]);
                        }
                        ).ToList();
                }
            }
        }
    }
}