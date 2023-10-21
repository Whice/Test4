using System.Collections.Generic;
using UnityEngine;
using View;

namespace Providers
{
    [CreateAssetMenu(fileName = "BonusViewProvider", menuName = "Game view/BonusViewProvider")]
    public class BonusViewProvider : ScriptableObject
    {
        #region Объекты в провайдере.

        [SerializeField]
        private BonusView[] resourcesList = new BonusView[0];
        private Dictionary<int, BonusView> resourcesDictionaryField = null;
        private Dictionary<int, BonusView> resourcesDictionary
        {
            get
            {
                if (resourcesDictionaryField == null)
                {
                    resourcesDictionaryField = new Dictionary<int, BonusView>(resourcesList.Length);
                    foreach (BonusView view in resourcesList)
                    {
                        resourcesDictionaryField.Add(view.id, view);
                    }
                }
                return resourcesDictionaryField;
            }
        }

        #endregion Объекты в провайдере.

        private int[] _ids;
        public int[] ids
        {
            get
            {
                if (_ids == null || _ids.Length == 0)
                {
                    _ids = new int[resourcesList.Length];
                    for (int i = 0; i < resourcesList.Length; i++)
                    {
                        _ids[i] = resourcesList[i].id;
                    }
                }
                return _ids;
            }
        }

        /// <summary>
        /// Получить клон префаба с указанным id.
        /// </summary>
        public virtual BonusView GetPrefabClone(int id)
        {
            if (resourcesDictionary.ContainsKey(id))
            {
                return Instantiate(resourcesDictionary[id]);
            }
            else
            {
                Debug.Log("In " + nameof(name) + " not found prefab with id: " + id + "!");
                return null;
            }
        }
    }
}