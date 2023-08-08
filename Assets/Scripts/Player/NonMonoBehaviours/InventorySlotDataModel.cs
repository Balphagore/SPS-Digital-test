using System;
using UnityEngine;

namespace SPSDigital.Player
{
    [Serializable]
    public class InventorySlotDataModel
    {
        public string Name;
        public EItemType ItemType;
        public int Level;
        public Sprite Sprite;
    }
}
