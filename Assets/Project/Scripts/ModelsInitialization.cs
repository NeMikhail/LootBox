using AxGrid.Base;
using System.Collections.Generic;
using UnityEngine;

namespace LootBox
{
    public class ModelsInitialization : MonoBehaviourExt
    {
        [SerializeField] private ItemsList _itemsList;
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private GameObject _itemFramePrefab;
        [SerializeField] private GameObject _effectsObject;

        [OnAwake]
        void Init()
        {
            InitItemsList();
        }

        private void InitItemsList()
        {
            Dictionary<ItemID, Item> itemsDict = new Dictionary<ItemID, Item>();
            foreach (Item item in _itemsList.Items)
            {
                itemsDict.Add(item.ID, item);
            }
            Model.Set(ConsatantStrings.D_ITEMS_LIST, itemsDict);
            Model.Set(ConsatantStrings.C_CONTENT_RECT_TRANSFORM, _contentTransform);
            Model.Set(ConsatantStrings.GO_ITEM_FRAME_PREFAB, _itemFramePrefab);
            Model.Set(ConsatantStrings.GO_EFFECTS_OBJECT, _effectsObject);
        }
    }
}

