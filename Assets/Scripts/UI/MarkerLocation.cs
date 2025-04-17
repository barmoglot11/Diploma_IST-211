using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class MarkerLocation
    {
        public string questID;

        /// <summary>
        /// Точки нахождения маркера в пространстве на стадии квеста
        /// </summary>
        public SerializedDictionary<int, Vector3> points;
        public SerializedDictionary<int, bool> isActiveAtStage;
    }
}