using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "MarkerConf", menuName = "Marker", order = 0)]
    public class MarkerConfigData : ScriptableObject
    {
        public List<MarkerLocation> markerLocations;
        
        public MarkerLocation GetConfig(string questID)
        {
            questID = questID.ToLower();

            return markerLocations.FirstOrDefault(data => string.Equals(questID, data.questID.ToLower()));
        }
    }
}