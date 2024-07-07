using AxGrid;
using AxGrid.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LootBox
{
    [State(ConsatantStrings.S_INIT_STATE)]
    public class MainInitState : FSMState
    {
        private RectTransform _contentTransform;
        private GameObject _itemFramePrefab;
        private List<ItemView> _itemViewsList;

        [Enter]
        private void EnterThis()
        {
            InitFields();
            CreateItemsInScroll();
            Model?.EventManager.AddAction(ConsatantStrings.E_ON_START_ROLL_CLICK, StartRoll);
            Model?.EventManager.AddAction(ConsatantStrings.E_ON_STOP_ROLL_CLICK, StopRoll);
            Parent.Change(ConsatantStrings.S_WAITING_STATE);
            Model.Set(ConsatantStrings.B_CAN_CHANGE_STATE, true);
        }

        private void InitFields()
        {
            _contentTransform = Model.Get<RectTransform>
                (ConsatantStrings.C_CONTENT_RECT_TRANSFORM);
            _itemFramePrefab = Model.Get<GameObject>
                (ConsatantStrings.GO_ITEM_FRAME_PREFAB);
            _itemViewsList = new List<ItemView>();
        }
        private void CreateItemsInScroll()
        {
            Dictionary<ItemID, Item> items = Model.Get<Dictionary<ItemID, Item>>(
                ConsatantStrings.D_ITEMS_LIST);
            foreach (KeyValuePair<ItemID, Item> item in items)
            {
                GameObject ItemIcon = GameObject.Instantiate(_itemFramePrefab,
                    _contentTransform);
                ItemView itemView = ItemIcon.GetComponent<ItemView>();
                itemView.ChangeImage(item.Value.ItemSprite);
                itemView.ID = item.Key;
                _itemViewsList.Add(itemView);
            }
            Model.Set(ConsatantStrings.D_ITEM_VIEWS, _itemViewsList);
        }

        [Exit]
        private void ExitThis()
        {

        }

        private void StartRoll()
        {
            if (Parent.CurrentStateName == ConsatantStrings.S_WAITING_STATE
                && Model.Get<bool>(ConsatantStrings.B_CAN_CHANGE_STATE))
            {
                Parent.Change(ConsatantStrings.S_ROLLING_STATE);
                Model.Set(ConsatantStrings.B_CAN_CHANGE_STATE, false);
            }
        }

        private void StopRoll()
        {
            if (Parent.CurrentStateName == ConsatantStrings.S_ROLLING_STATE
                && Model.Get<bool>(ConsatantStrings.B_CAN_CHANGE_STATE))
            {
                Parent.Change(ConsatantStrings.S_REWARD_STATE);
            }
        }
    }
}




