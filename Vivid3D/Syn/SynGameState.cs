using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Vivid;
using Vivid.Structs.Game;
namespace Vivid.SynWave
{
    public class SynGameState
    {

        public StructGameState State;

        public SynGameState(StructGameState state)
        {
            State = state;
        }

        public void Begin()
        {

            SynHost.Active.GameState = this;

            State.Begin();




        }

        public void Update()
        {
            State.Update();
        }

        public void Draw()
        {
            State.Draw();
        }

    }
}
