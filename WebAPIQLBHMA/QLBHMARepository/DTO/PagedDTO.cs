﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBHMARepository.DTO
{
    public class PagedInput
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
    public class pagedInputByLoaiId
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Id { get; set; }
    }
    public class PagedOutput<T>
    {
        public List<T> Items { get; set; }
        public int TotalItemCount { get; set; }
    }
}
