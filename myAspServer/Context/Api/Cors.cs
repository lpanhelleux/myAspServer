namespace myAspServer.Context.Api
{
    public static class Cors
    {
        public static string Build(WebApplicationBuilder builder)
        {

            var policy = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: policy,
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:3000", "http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            return policy;
        }
    }
}