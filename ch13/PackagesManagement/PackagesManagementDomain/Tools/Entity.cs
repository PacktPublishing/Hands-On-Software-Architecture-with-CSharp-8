using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDD.DomainLayer
{
    public abstract class Entity<K>: IEntity<K>
        where K: IEquatable<K>
    {
        
        public virtual K Id { get; protected set; }
        public bool IsTransient()
        {
            return Object.Equals(Id, default(K));
            
        }
        public override bool Equals(object obj)
        {

            if (obj == null || !(obj is Entity<K>))
                return false;
            if (Object.ReferenceEquals(this, obj))
                return true;
            if (this.GetType() != obj.GetType())
                return false;
            Entity<K> item = (Entity<K>)obj;
            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return Object.Equals(item.Id, Id);
        }
        int? _requestedHashCode;
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;
                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }
        public static bool operator ==(Entity<K> left, Entity<K> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null));
            else
                return left.Equals(right);
        }
        public static bool operator !=(Entity<K> left, Entity<K> right)
        {
            return !(left == right);
        }
        [NotMapped]
        public List<IEventNotification> DomainEvents { get; private set; }
        public void AddDomainEvent(IEventNotification evt)
        {
            DomainEvents = DomainEvents ?? new List<IEventNotification>();
            DomainEvents.Add(evt);
        }
        public void RemoveDomainEvent(IEventNotification evt)
        {
            DomainEvents?.Remove(evt);
        }
    }
}
