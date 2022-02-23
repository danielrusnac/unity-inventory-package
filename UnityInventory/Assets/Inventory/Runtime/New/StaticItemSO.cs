﻿using UnityEngine;

namespace InventorySystem.New
{
    [CreateAssetMenu(fileName = "Static Item", menuName = InventoryConstants.CREATE_ITEMS_SUB_MENU + "Static Item")]
    public class StaticItemSO : ItemSO
    {
        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private string _glyph;
        [SerializeField] private Sprite _icon;

        public override int ID => _id;
        public string Glyph => _glyph;
        public Sprite Icon => _icon;

        public override int DynamicID => ID;
        public override string Name => _name;

        public override ItemSO GetInstance()
        {
            return this;
        }
    }
}