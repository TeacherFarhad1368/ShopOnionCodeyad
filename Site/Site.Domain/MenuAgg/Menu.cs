using Shared.Domain;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.MenuAgg
{
    public class Menu : BaseEntity<int>
    {
        public Menu(int number, string title, string url, MenuStatus status,
             string? imageName, string? imageAlt, int? parentId)
        {
            Number = number;
            Title = title;
            Url = url;
            Status = status;
            Active = true;
            ImageName = imageName;
            ImageAlt = imageAlt;
            ParentId = parentId;
        }
        public void Edit(int number, string title, string url, 
             string? imageName, string? imageAlt)
        {
            Number = number;
            Title = title;
            Url = url;
            Active = true;
            ImageName = imageName;
            ImageAlt = imageAlt;
        }
        public void ActivationChange()
        {
            if(Active) Active = false;
            else Active = true;
        }
        public Menu()
        {
            
        }
        public int Number { get; private set; }
        public string Title { get; private set; }
        public string Url { get; private set; }
        public MenuStatus Status { get; private set; }
        public bool Active { get; private set; }
        public string? ImageName { get; private set; }
        public string? ImageAlt { get; private set; }
        public int? ParentId { get; private set; }
        public Menu? Parent { get; private set; }
        public List<Menu>? Childs { get; private set; }
    }
}
