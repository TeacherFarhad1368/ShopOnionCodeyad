using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ModelName { get; set; }
        public OperationResult(bool success, string? message = "", string? modelName = "")
        {
            Success = success;
            Message = message;
            ModelName = modelName;
        }
    }
    public class OperationResultfactor
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public OperationResultfactor(bool success, string? message = "", string? url = "")
        {
            Success = success;
            Message = message;
            Url = url;
        }
    }
    public class OperationResultOrderDiscount
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public int Id { get; set; }
        public int Percent { get; set; }
        public OperationResultOrderDiscount(bool success, string? message = "", string? title = "", int id = 0,int percent = 0)
        {
            Success = success;
            Message = message;
            Id = id;
            Percent = percent;
            Title = title;
        }
    }
    public class OperationResultWithKey
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ModelName { get; set; }
        public int Id { get; set; }
        public OperationResultWithKey(bool success, string? message = "", string? modelName = "" , int id = 0)
        {
            Success = success;
            Message = message;
            ModelName = modelName;
            Id = id;
        }
    }
    public class OperationResultWithKeylong
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ModelName { get; set; }
        public long Id { get; set; }
        public OperationResultWithKeylong(bool success, string? message = "", string? modelName = "", long id = 0)
        {
            Success = success;
            Message = message;
            ModelName = modelName;
            Id = id;
        }
    }
}
