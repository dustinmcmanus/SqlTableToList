using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlTableToList.Examples
{
    [Table("USER_SETTING")]
    public class UserSetting
    {
        [Column("SETTING_NAME")]
        public virtual string SettingName { get; set; }

        [Column("VALUE")]
        public virtual string Value { get; set; }

        [Column("USER_NAME")]
        public virtual string UserName { get; set; }
    }
}
