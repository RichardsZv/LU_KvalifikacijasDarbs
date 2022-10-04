namespace SupervaroniApp.Data
{
    public class AtskaiteData
    {
        public IConfiguration Configuration;
        public AtskaiteData(IConfiguration configuration)
        {
            Configuration = configuration; //Inject configuration to access Connection string from appsettings.json.
        }
    }
}
