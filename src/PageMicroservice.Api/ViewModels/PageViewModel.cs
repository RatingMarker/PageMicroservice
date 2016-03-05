﻿using System;

namespace PageMicroservice.Api.ViewModels
{
    public class PageViewModel
    {
        public int PageId { get; set; }
        public string Uri { get; set; }
        public DateTime? FoundDate { get; set; }
        public DateTime? LastScanDate { get; set; }
        public int SiteId { get; set; }
        public SiteViewModel Site { get; set; }
    }
}