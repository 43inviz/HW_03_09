namespace HW_03_09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DBManager dbManager = new DBManager();

            //dbManager.EnsurePopulate();


            //Get user + related entity by id 
            var result = dbManager.GetUserAndRelated(2);


            //Delete user + reslated entity by id
            dbManager.DeleteUserById(3);

        }
    }
}
