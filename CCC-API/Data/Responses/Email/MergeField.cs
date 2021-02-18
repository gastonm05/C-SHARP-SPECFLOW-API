namespace CCC_API.Data.Responses.Email
{
    public class MergeField
    {
        public string Label { get; set; }
        public string Value { get; set; }

        protected bool Equals(MergeField other)
        {
            return string.Equals(Label, other.Label) && string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MergeField) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Label != null ? Label.GetHashCode() : 0) * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"{Label} {Value}";
        }
    }
}