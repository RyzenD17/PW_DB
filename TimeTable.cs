//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PW10_DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class TimeTable
    {
        public int ID { get; set; }
        public int IDWorkTime { get; set; }
        public int IDOrder { get; set; }
    
        public virtual OrdersTable OrdersTable { get; set; }
        public virtual WorkTimeTable WorkTimeTable { get; set; }
    }
}
