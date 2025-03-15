using System;
using UnityEngine;

namespace INVESTIGATION
{
    public class InvestObject : MonoBehaviour
    {
        public GameObject Obj;

        private bool ObjEnabled => Obj.activeSelf;
        private bool Investigating => InvestigationManager.Instance.Investigating;
        public void Update()
        {
            if(!ObjEnabled && Investigating)
                Obj.SetActive(true);
            if(ObjEnabled && !Investigating)
                Obj.SetActive(false);
        }
    }
}