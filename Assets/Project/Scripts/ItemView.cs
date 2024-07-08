using UnityEngine;
using UnityEngine.UI;

namespace LootBox
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private RectTransform _rectTransform;
        private ItemID _itemID;

        public ItemID ID
        {
            get => _itemID;
            set => _itemID = value;
        }
        public RectTransform RectTransform
        {
            get => _rectTransform;
        }

        public void ChangeImage(Sprite image)
        {
            _iconImage.sprite = image;
            _iconImage.SetNativeSize();
        }
    }
}

