﻿using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
    public class ItemSO : ScriptableObject
    {
        [SerializeField] private int _id;

        [Tooltip("Will be used to display the name in the UI.")]
        [SerializeField] private string _itemName;

        [SerializeField] private string _glyph;
        [SerializeField] private Sprite _normalIcon;
        [SerializeField] private Sprite _lockedIcon;

        public int ID => _id;
        public string ItemName => _itemName;
        public string Glyph => _glyph;

        public virtual Sprite GetIcon(IconType type = IconType.Normal)
        {
            switch (type)
            {
                case IconType.Locked:
                    return _lockedIcon;
            }

            return _normalIcon;
        }

        protected virtual void Reset()
        {
            RefreshID();
        }
        
        [ContextMenu("Refresh ID")]
        private void RefreshID()
        {
#if UNITY_EDITOR
            _id = Guid.NewGuid().GetHashCode();
            EditorUtility.SetDirty(this);
#endif
        }
    }
}