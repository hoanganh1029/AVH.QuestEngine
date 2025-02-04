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
                    ["playerId"] = new OpenApiString("0b5a9152-414a-41ff-b198-b8a707a4f90c"),
                    ["playerLevel"] = new OpenApiInteger(10),
                    ["chipAmountBet"] = new OpenApiInteger(8386)
                };
            }
        }
    }
}
