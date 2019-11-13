using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
namespace Vivid.Scene.Anim.FrameTypes
{
    public class NodeFrame : AnimFrame 
    {

        public Node3D Node
        {
            get;
            set;
        }

        public Vector3 Pos
        {
            get;
            set;
        }

        public Matrix4 RotP
        {
            get;
            set;
        }

    }
}
