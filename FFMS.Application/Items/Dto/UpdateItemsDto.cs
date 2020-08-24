using System;
using System.Collections.Generic;
using System.Text;

namespace FFMS.Application.Items.Dto
{
    public class UpdateItemsDto: BasItemsDto
    {
        public int ID { get; set; }
        public string OldItemType { get; set; }
    }
}
