using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{

    /// <summary>
    /// 定义默认主键类型为Guid的实体基类
    /// </summary>
    public abstract class BaseEntity 
    {
        [Key]
        public Guid guid { get; set; }
        public Guid created_by { get; set; }
        public DateTime created_on { get; set; }
        public Guid updated_by { get; set; }
        public DateTime updated_on { get; set; }
        public string ip_address { get; set; }
        public bool is_deleted { get; set; }
    }

}
