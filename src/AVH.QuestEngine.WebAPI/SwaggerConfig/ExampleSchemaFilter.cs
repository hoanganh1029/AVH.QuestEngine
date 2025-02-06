using AVH.QuestEngine.Application.Constants;
using AVH.QuestEngine.Application.Requests;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AVH.QuestEngine.WebAPI.SwaggerConfig
{
    public class ExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(ProgressRequest))
            {
                schema.Example = new OpenApiObject()
                {
                    ["PlayerId"] = new OpenApiString(Constant.DefaultPlayerId.ToString()),
                    ["PlayerLevel"] = new OpenApiInteger(5),
                    ["ChipAmountBet"] = new OpenApiInteger(100)
                };
            }
        }
    }
}
