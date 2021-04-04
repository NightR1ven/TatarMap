//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TatarCulturaWpf.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.Keys = new HashSet<Key>();
            this.Users = new HashSet<User>();
        }
    
        public int IdEvent { get; set; }
        public string Name { get; set; }
        public Nullable<int> Coin { get; set; }
        public Nullable<System.DateTime> DateStarEvent { get; set; }
        public Nullable<System.DateTime> DateEndEvent { get; set; }
        public string EventPhoto { get; set; }
        public Nullable<int> IdObject { get; set; }

        public string GetEventPhoto
        {
            get
            {
                if (EventPhoto is null)
                    return null;
                return Directory.GetCurrentDirectory() + @"\ImagesEvent\" + EventPhoto.Trim();
            }
        }

        public bool GetDate
        {
            get
            {
                if (DateStarEvent <= DateTime.Now && DateTime.Now <= DateEndEvent)
                    return true;
                else
                    return false;

            }
        }

        public string GetColor
        {
            get
            {
                if (DateStarEvent <= DateTime.Now && DateTime.Now <= DateEndEvent)
                    return "#FFE9A865";
                else
                    return "#262626";

            }
        }

        public string GetKeyColor
        {
            get
            {
                int x = 0;
                foreach (Key z in Keys)
                {
                    if (z.Active == false)
                        x++;
                }

                if (x == 0)
                    return "#262626";

                return "#FFE9A865";

            }
        }

        public string GetKeyLabel
        {
            get
            {
                int x = 0;
                foreach (Key z in Keys)
                {
                    if (z.Active == false)
                        x++;
                }

                if (x == 0)
                    return "Коды закончились";

                return "Приобрести";

            }
        }

        public string GetDateLabel
        {
            get
            {

                if (DateStarEvent <= DateTime.Now && DateTime.Now <= DateEndEvent)
                    return "Приобрести";

                return "Акция закончилась";
            }
        }

        public string GetKeyVisible
        {
            get
            {
                if (DateStarEvent <= DateTime.Now && DateTime.Now <= DateEndEvent)
                    return "Visible";

                else
                    return "Collapsed";

            }
        }

        public string GetDateVisible
        {
            get
            {
                if (DateStarEvent <= DateTime.Now && DateTime.Now <= DateEndEvent)
                    return "Collapsed";
                else
                    return "Visible";

            }
        }

        public bool GetKeyCheck
        {
            get
            {
                int x = 0;
                foreach (Key z in Keys)
                {
                    if (z.Active == false)
                        x++;
                }

                if (x == 0)
                    return false;

                return true;

            }
        }

        public string GetTypeObject
        {
            get
            {
                return Object.Type.Name;
            }
        }

        public virtual Object Object { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Key> Keys { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
