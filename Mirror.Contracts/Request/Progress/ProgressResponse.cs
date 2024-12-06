﻿using Mirror.Contracts.Request.ProgressValue;

namespace Mirror.Contracts.Request.Progress
{
    public class ProgressResponse
    {
        public string Description { get; set; } = null!;

        public string ProgressColumnHead { get; set; } = null!;

        public List<ProgressValueDTO> ProgressValue { get; set; } = [];
    }
}