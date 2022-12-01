using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectWebCuoiKhoa.Models;

namespace ProjectWebCuoiKhoa.Models
{
    public class DetailViewProduct
    {
        public IEnumerable<ProjectWebCuoiKhoa.Models.Product> Products { get; set; }
        public ProjectWebCuoiKhoa.Models.Product Product { get; set; }
    }
}