using System;
using System.Collections;
using System.Text;
using UnityEngine;

namespace DefaultNamespace
{
    public class BinaryTest : MonoBehaviour
    {
        [SerializeField] private int number;
        
        
        [ContextMenu("Bit")]
        private void Bit()
        {
            var bitArray = new BitArray(new int[]{number});
            var sb = new StringBuilder();
            foreach (bool b in bitArray)
                sb.Append(b ? 1 : 0);
            Debug.Log(sb);
            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            Debug.Log(array[0]);
        }
    }
}