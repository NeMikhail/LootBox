using AxGrid.Base;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LootBox
{
    public class ModelsInitialization : MonoBehaviourExt
    {
        [SerializeField] private ItemsList _itemsList;
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private GameObject _itemFramePrefab;
        [SerializeField] private GameObject _effectsObject;
        [SerializeField] private VerticalLayoutGroup _layout;

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
            Model.Set(ConsatantStrings.C_VERTICAL_LAYOUT, _layout);
        }
    }
}

