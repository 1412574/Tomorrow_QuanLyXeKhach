using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class CongViec
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã công việc", ShortName = "Mã số")]
        public int maCV { get; set; }


        [StringLength(100)]
        [Display(Name = "Tên công việc", ShortName = "Tên")]
        public string tenCV { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Yêu cầu công việc", ShortName = "Yêu cầu")]
        public string yeuCauCV { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Mô tả công việc", ShortName = "Mô tả")]
        public string moTaCV { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày hoàn thành", ShortName = "Ngày hoàn thành")]
        public DateTime hanHoanThanh { get; set; }

        public virtual ICollection<PhanCong> PhanCongs { get; set; }

        public int trangThai { get { return (hanHoanThanh > DateTime.Now) ? 2 : 1; } }

        public string ShortString
        {
            get
            {
                return String.Format("{0} - {1}", maCV, tenCV);
            }
        }
    }
}
