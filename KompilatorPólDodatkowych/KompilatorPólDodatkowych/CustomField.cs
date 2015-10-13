
namespace KompilatorPólDodatkowych
{
    public class CustomField
    {

        public CustomField()
        {
            DatabaseField = "1";
            ObjectType = "2";
        }

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.LOE = value;
            }
        }
        public string ObjectType { get; set; }

        private string loe;
        public string LOE
        {
            get
            {
                return this.loe;
            }
            private set
            {
                loe = value;

            }
        }

        private string dataType;
        public string DataType
        {
            get
            {
                return this.dataType;
            }
            set
            {
                this.dataType = value;
                switch (value)
                {
                    case "1":
                        this.DisplayFormat = "1"; break;
                    case "5":
                        this.DisplayFormat = "5"; break;
                    default:
                        this.DisplayFormat = "0"; break;
                }
            }

        }
        public string DisplayFormat { get; private set; }
        public string DatabaseField { get; private set; }

    }
}
