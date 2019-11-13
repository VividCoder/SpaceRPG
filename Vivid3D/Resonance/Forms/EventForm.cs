using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Resonance.Forms
{
    public class EventForm : UIForm
    {
        public EventOp Op = null;
        public EventForm(EventOp op = null)
        {

            Op = op;

            Update = () =>
            {

                Op?.Invoke();

            };

        }



    }

    public delegate void EventOp();


}
