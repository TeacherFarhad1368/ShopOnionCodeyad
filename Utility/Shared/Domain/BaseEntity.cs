using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; private set; }
    }
    public class BaseEntityCreate<Tkey> : BaseEntity<Tkey>
    {
        public DateTime CreateDate { get; private set; }
        public BaseEntityCreate()
        {
            CreateDate = DateTime.Now;
        }
    }
    public class BaseEntityCreateUpdate<Tkey> : BaseEntityCreate<Tkey>
    {
        public DateTime UpdateDate { get; private set; }
        public BaseEntityCreateUpdate()
        {
            UpdateDate = DateTime.Now;
        }
        public void UpdateEntity()
        {
            UpdateDate = DateTime.Now;
        }
    }
    public class BaseEntityCreateUpdateActive<Tkey> : BaseEntityCreateUpdate<Tkey>
    {
        public bool Active { get; private set; }
        public BaseEntityCreateUpdateActive()
        {
            Active = true;  
        }
        public void ActivationChange()
        {
            if (Active) Active = false;
            else Active = true;
        }
    }
    public class BaseEntityCreateActive<Tkey> : BaseEntityCreate<Tkey>
    {
        public bool Active { get; private set; }
        public BaseEntityCreateActive()
        {
            Active = true;
        }
        public void ActivationChange()
        {
            if (Active) Active = false;
            else Active = true;
        }
    }
    public class BaseEntityActive<Tkey> : BaseEntity<Tkey>
    {
        public bool Active { get; private set; }
        public BaseEntityActive()
        {
            Active = true;
        }
        public void ActivationChange()
        {
            if (Active) Active = false;
            else Active = true;
        }
    }
}
