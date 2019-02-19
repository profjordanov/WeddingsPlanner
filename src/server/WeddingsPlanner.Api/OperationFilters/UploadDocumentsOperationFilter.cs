using Optional.Collections;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using WeddingsPlanner.Core.Models;

namespace WeddingsPlanner.Api.OperationFilters
{
    public class UploadDocumentsOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var fileParameter = operation
                .Parameters
                .FirstOrNone(p => string.Equals(p.Name, nameof(UploadDocumentRequest.File), System.StringComparison.OrdinalIgnoreCase));

            fileParameter.MatchSome(parameter =>
            {
                operation.Parameters.Remove(parameter);

                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "File",
                    In = "formData",
                    Description = "Upload File",
                    Required = true,
                    Type = "file"
                });

                operation.Consumes.Add("multipart/form-data");
            });
        }
    }
}