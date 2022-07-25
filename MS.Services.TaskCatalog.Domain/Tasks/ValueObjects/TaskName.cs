using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Domain.Tasks.Exceptions.Domain;
using MsftFramework.Core.Exception;

namespace MS.Services.TaskCatalog.Domain.Tasks.ValueObjects
{

    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject other = (ValueObject)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^
                    ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }

                if (thisValues.Current != null &&
                    !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }
        // Other utility methods
    }
    public class TaskName : ValueObject
    {
        public string? Value { get; private set; }

        public TaskName? Null => null;

        public static TaskName Create(string value)
        {
            return new TaskName
            {
                Value = Guard.Against.NullOrEmpty(value, new TaskDomainException("Name can't be null mor empty."))
            };
        }

        public static implicit operator TaskName(string value) => Create(value);

        public static implicit operator string(TaskName value) =>
            Guard.Against.Null(value.Value!, new TaskDomainException("Name can't be null."));
        public override string ToString()
        {
            return $"{Value}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Value!;
        }
    }
}