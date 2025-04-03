using System;
using UnityEngine;
using LOCKPICKING;
namespace _TEST_
{
    public class LockTest : MonoBehaviour
    {
        private void Start()
        {
            InputManager.Instance.ChangeInputStatus(InputStatus.Lock);
            LockpickingManager.Instance.Picking();
        }
    }
}