namespace HomeWork.IoC
{
    using System;
    using System.Collections.Generic;

    internal class ClosedGenericTypeKey
    {
        public ClosedGenericTypeKey(Type unboundGenericType, Type closedGenericType)
        {
            this.UnboundGenericType = unboundGenericType;
            this.ClosedGenericType = closedGenericType;
        }

        public Type UnboundGenericType { get; set; } 

        public Type ClosedGenericType { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ClosedGenericTypeKey key &&
                   EqualityComparer<Type>.Default.Equals(this.UnboundGenericType, key.UnboundGenericType) &&
                   EqualityComparer<Type>.Default.Equals(this.ClosedGenericType, key.ClosedGenericType);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.UnboundGenericType, this.ClosedGenericType);
        }
    }
}
