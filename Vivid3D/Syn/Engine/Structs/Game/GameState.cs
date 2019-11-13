using System.Collections.Generic;
namespace Vivid.Structs.Game
{
    public class StructGameState : Struct
    {

        public static StructGameState Active = null;

        public StructStateSub InitS, UpdateS, DrawS;

        public List<StructSub> RunningSubs = new List<StructSub>();

        public string Name
        {
            get;
            set;
        }

        public bool Init
        {
            get;
            set;
        }

        public StructCode Code
        {
            get;
            set;
        }

        public bool Running
        {
            get;
            set;
        }

        public bool Run = false;

        public void Begin()
        {
            InitS.Code.Exec();
         //   Code.LineNum = 0;
        }

        public dynamic ActiveSub = null;

        public void Update()
        {

            UpdateS.Code.Exec();
            //Active = this;
            //StructSub.SubRoot = this;
           // Code.Exec();
        }

        public void Draw()
        {

            DrawS.Code.Exec();

        }
    }
}