namespace PMCMS.DAL
{
    public class DBTool
    {
        private DBTool() { }


        private static PocketMenuDBEntities _instance;

        public static PocketMenuDBEntities GetInstance()
        {
            if (_instance == null)
                _instance = new PocketMenuDBEntities();

            return _instance;
        }
    }
}
