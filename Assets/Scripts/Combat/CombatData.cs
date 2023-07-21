using System;
using UnityEngine;

namespace Combat
{
    [Serializable]
    public struct CombatData
    {
        [SerializeField] private float fireRateInSeconds;

        public float FireRateInSeconds => fireRateInSeconds;
    }
}