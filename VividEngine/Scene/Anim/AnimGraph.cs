using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivid.Scene.Anim
{
    public class AnimGraph
    {

        public SceneGraph3D VisualGraph
        {
            get;
            set;
        }

        public List<AnimNode> Nodes = new List<AnimNode>();

        public double AnimLength = 30.0;

        public string Name = "New Animation";


    }
}
