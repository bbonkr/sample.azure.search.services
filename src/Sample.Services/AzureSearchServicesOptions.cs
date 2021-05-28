namespace Sample.Services
{
    public class AzureSearchServicesOptions
    {
        public static string Name = "AzureSearchServices";

        public string ServiceName { get; set; }
        public string IndexName { get; set; }

        public string ApiKey { get; set; }
    }
}
