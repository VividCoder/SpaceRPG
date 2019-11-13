namespace Vivid.Structs
{
    public class StructSub : Struct
    {

        public static dynamic SubRoot = null;

        public string Name
        {
            get;
            set;
        }

        public StructCode Code
        { get; set; }

        public bool Threaded = false;
        public bool Loop = false;
        public bool Ran = false;
        public override dynamic Exec()
        {
            if (SubRoot != null)
            {
                SubRoot.ActiveSub = this;
            }
            if (Loop || Ran == false)
            {
                Code.Exec();
                Ran = true;
            }
            return null;
            // base.Exec();


        }

    }
}