using System.Collections.Generic;
using UnityEngine;

namespace LootBox
{
    [CreateAssetMenu(fileName = "ItemsList", menuName = "Scriptables/ItemsList", order = 0)]
    public class ItemsList : ScriptableObject
    {
        [SerializeField] private List<Item> _items;

        public List<Item> Items
        {
            get => _items;
        }
    }
}
