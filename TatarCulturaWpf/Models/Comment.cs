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
    
    public partial class Comment
    {
        public int IdComment { get; set; }
        public Nullable<int> IdUser { get; set; }
        public Nullable<int> IdObject { get; set; }
        public string Comment1 { get; set; }
    
        public virtual User User { get; set; }
        public virtual Object Object { get; set; }
    }
}
