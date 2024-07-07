using System.Collections;
using UnityEngine;

namespace LootBox
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptables/Item", order = 0)]
    public class Item : ScriptableObject
    {
        [SerializeField] private ItemID _id;
        [SerializeField] private Sprite _sprite;

        public ItemID ID
        {
            get => _id;
        }

        public Sprite ItemSprite
        {
            get => _sprite;
        }
    }
}

