using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid.Scene.Anim.FrameTypes;
namespace Vivid.Scene.Anim.Nodes
{
    public class NodeAnim : AnimNode
    {

        public Node3D Node
        {
            get;
            set;
        }

        public List<NodeFrame> Frames = new List<NodeFrame>();

        public override string GetName()
        {
            return "Node3D:"+Node.Name;
        }

    }
}
