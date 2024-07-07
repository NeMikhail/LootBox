using AxGrid;
using AxGrid.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LootBox
{
    [State(ConsatantStrings.S_ROLLING_STATE)]
    public class BoxRollingState : FSMState
    {
        private RectTransform _contentTransform;
        private GameObject _itemFramePrefab;
        private List<ItemView> _itemViewsList;
        private GameObject _effectsObject;

        private int _upperIndex;
        private float _rollSpeed;
        private float _rollMaxSpeed = 5f;
        private float _halfSizeDelta;
        private float _sizeDelta;


        [Enter]
        private void EnterThis()
        {
            InitFields();
            //Можно было сделать через [Loop], я сделал через подписку
            Settings.Model.EventManager.AddAction(ConsatantStrings.E_EXECUTE_BOX_STATE,
                Execute);
            _effectsObject.SetActive(false);
        }

        private void ChangeState()
        {
            Parent.Change(ConsatantStrings.S_REWARD_STATE);
        }

        [Exit]
        private void ExitThis()
        {
            Settings.Model.EventManager.RemoveAction(ConsatantStrings.E_EXECUTE_BOX_STATE,
                Execute);
            Model.Set(ConsatantStrings.D_ROLLSPEED, _rollSpeed);
            Model.Set(ConsatantStrings.D_UPPERINDEX, _upperIndex);
        }

        [One(5f)]
        private void Cooldown()
        {
            Model.Set(ConsatantStrings.B_CAN_CHANGE_STATE, true);
        }

        private void InitFields()
        {
            _contentTransform = Model.Get<RectTransform>
                (ConsatantStrings.C_CONTENT_RECT_TRANSFORM);
            _itemFramePrefab = Model.Get<GameObject>
                (ConsatantStrings.GO_ITEM_FRAME_PREFAB);
            _itemViewsList = Model.Get<List<ItemView>>
            (ConsatantStrings.D_ITEM_VIEWS);
            _effectsObject = Model.Get<GameObject>
                (ConsatantStrings.GO_EFFECTS_OBJECT);

            _halfSizeDelta =
                _itemFramePrefab.GetComponent<RectTransform>().sizeDelta.y / 2 +
                _contentTransform.gameObject.GetComponent<VerticalLayoutGroup>().
                padding.top;
            _sizeDelta = _itemFramePrefab.GetComponent<RectTransform>().sizeDelta.y +
                _contentTransform.gameObject.GetComponent<VerticalLayoutGroup>().
                padding.top;
            _contentTransform.anchoredPosition =
                new Vector3(_contentTransform.anchoredPosition.x,
                _contentTransform.rect.height);
            _upperIndex = _itemViewsList.Count - 1;
        }

        public void Execute()
        {
            TryReplaceFirstItem();
            if (_rollSpeed <= _rollMaxSpeed)
            {
                _rollSpeed += 0.002f;
            }
            _contentTransform.transform.Translate(new Vector3(0, -_rollSpeed));
        }

        private void StopAndCenter()
        {
            _rollSpeed = 0;
            float upperDelta =
                Mathf.Abs(_contentTransform.anchoredPosition.y - _sizeDelta * 2);
            float bottomDelta =
                Mathf.Abs(_contentTransform.anchoredPosition.y - _sizeDelta * 3);
            if (bottomDelta < upperDelta)
            {
                _contentTransform.anchoredPosition =
                    new Vector3(_contentTransform.anchoredPosition.x, _sizeDelta * 3);
            }
            else
            {
                _contentTransform.anchoredPosition =
                    new Vector3(_contentTransform.anchoredPosition.x, _sizeDelta * 2);
            }
        }

        private void TryReplaceFirstItem()
        {
            if (_contentTransform.anchoredPosition.y < _sizeDelta * 2)
            {
                _contentTransform.transform.Translate(new Vector3(0, _halfSizeDelta));
                _itemViewsList[_upperIndex].gameObject.transform.SetSiblingIndex(0);
                if (_upperIndex > 0)
                {
                    _upperIndex--;
                }
                else
                {
                    _upperIndex = _itemViewsList.Count - 1;
                }
            }
        }
    }
}




