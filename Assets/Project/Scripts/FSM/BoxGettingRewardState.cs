using AxGrid;
using AxGrid.FSM;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LootBox
{
    [State(ConsatantStrings.S_REWARD_STATE)]
    public class BoxGettingRewardState : FSMState
    {
        private RectTransform _contentTransform;
        private GameObject _itemFramePrefab;
        private List<ItemView> _itemViewsList;
        private GameObject _effectsObject;

        private int _upperIndex;
        private float _halfSizeDelta;
        private float _sizeDelta;
        private float _rollSpeed;


        [Enter]
        private void EnterThis()
        {
            InitFields();
            Settings.Model.EventManager.AddAction(ConsatantStrings.E_EXECUTE_BOX_STATE,
                Execute);
        }

        [One(10f)]
        private void ChangeState()
        {
            Parent.Change(ConsatantStrings.S_WAITING_STATE);
        }

        [Exit]
        private void ExitThis()
        {
            _effectsObject.SetActive(false);
            Settings.Model.EventManager.RemoveAction(ConsatantStrings.E_EXECUTE_BOX_STATE,
                Execute);
        }

        private void Execute()
        {
            TryReplaceFirstItem();
            if (_rollSpeed > 0)
            {
                _rollSpeed -= 0.002f;
            }
            else
            {
                StopAndCenter();
            }
            _contentTransform.transform.Translate(new Vector3(0, -_rollSpeed));
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
            _rollSpeed = Model.Get<float>(ConsatantStrings.D_ROLLSPEED);
            _upperIndex = Model.Get<int>(ConsatantStrings.D_UPPERINDEX);

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
            _effectsObject.SetActive(true);
        }
    }
}




