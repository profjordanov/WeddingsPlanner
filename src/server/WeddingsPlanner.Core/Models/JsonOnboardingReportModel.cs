﻿namespace WeddingsPlanner.Core.Models
{
    public class JsonOnboardingReportModel
    {
        public JsonOnboardingReportModel(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}