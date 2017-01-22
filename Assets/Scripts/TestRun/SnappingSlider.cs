using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.TestRun
{
	class SnappingSlider:Slider
	{
		public override void OnDrag(PointerEventData eventData)
		{
			
			base.OnDrag(eventData);
			if (this.value > 0.98)
			{
				eventData.pointerDrag = null;
				this.value = 0;
			}
		}
	}
}
