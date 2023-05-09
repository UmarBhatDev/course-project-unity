using System;
using UnityEngine;

namespace Features.Hints.Data
{
    [Serializable]
    public struct KeyBind
    {
        private readonly KeyType _keyType;
        private readonly KeyCode _keyCode;

        public KeyType KeyType => _keyType;
        public KeyCode KeyCode => _keyCode;

        public KeyBind(KeyType keyType, KeyCode keyCode)
        {
            _keyType = keyType;
            _keyCode = keyCode;
        }
    }
}