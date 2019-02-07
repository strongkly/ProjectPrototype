using System.Collections.Generic;
using UnityEngine;

namespace CrazyBox.Components
{
    public class ParasiteReusableItemList<TItemView, TItemData, THost>
        : ReusableItemList<TItemView, TItemData>
        where TItemView : ParasiteReusableItem<TItemData, THost>
    {
        public THost host{ get; protected set; }

        public ParasiteReusableItemList(THost host, Transform childrenRoot, 
            GameObject templateObject = null)
            :base(childrenRoot, templateObject)
        {
            this.host = host;
            this.templateObject.GetComponent<TItemView>().SetHost(host);
        }

        protected override TItemView AddNewItem()
        {
            TItemView result = base.AddNewItem();
            result.SetHost(host);
            return result;
        }
    }

    public class ParasiteReusableItem<TItemData, THost> 
        : ReusableItem<TItemData>
    {
        public THost host { get; protected set; }

        public void SetHost(THost host)
        {
            this.host = host;
        }
    }
}
