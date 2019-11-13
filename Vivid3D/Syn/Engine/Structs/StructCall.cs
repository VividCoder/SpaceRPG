namespace Vivid.Structs
{
    public class StructCall : Struct
    {
        public string CallName
        {
            get;
            set;
        }

        public StructSub SubLU
        {
            get;
            set;
        }

        public override dynamic Exec()
        {
            SubLU.Code.Exec();

            return null;
        }
    }
}