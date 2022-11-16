using System;
using System.Threading.Tasks;
using Cards;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Factories
{
    public class CardFactory :  Singleton<CardFactory>
    {
        private const string _path = "Assets/Prefabs/";
        public bool initialised = false;

        public static void FabricateCard(Card card, string key, Vector3 position, Transform parent = null, Action<MonoCard> callback = null)
        {

            Addressables.InstantiateAsync(_path + key + ".prefab").Completed += (obj) =>
            {
                var cardObj = obj.Result.GetComponent<MonoCard>();
                cardObj.UpdateRenderData(card);
                cardObj.transform.name = cardObj.cardText_main.text;
                cardObj.transform.position = position;
                cardObj.transform.SetParent(parent);

                callback(cardObj);
            };
        }
    }
}