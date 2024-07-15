using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TK
{
    public class Utility_DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] float timeUntilDestroy = 5;

        private void Awake()
        {
            Destroy(gameObject, timeUntilDestroy);
        }
    }

}