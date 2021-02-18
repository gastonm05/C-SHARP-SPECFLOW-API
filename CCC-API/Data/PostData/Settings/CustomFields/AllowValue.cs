namespace CCC_API.Data.PostData.Settings.CustomFields
{
    public class AllowValue
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public AllowValue(string value) {
            Value = value;
        }

        public AllowValue(){}

        protected bool Equals(AllowValue other)
        {
            return Id == other.Id && string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AllowValue) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 367) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"Id: {Id}, Value: {Value}";
        }
    }
}
