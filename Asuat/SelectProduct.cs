//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Asuat
{
    using System;
    using System.Collections.Generic;
    
    public partial class SelectProduct
    {
        public int ID { get; set; }
        public Nullable<int> ID_Client { get; set; }
        public Nullable<int> ID_Products { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Product Product { get; set; }
    }
}