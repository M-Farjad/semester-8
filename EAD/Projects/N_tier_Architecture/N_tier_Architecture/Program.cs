
using Bussiness_objects;
using Dataaccess_layer;
using Bussinesslogic_layer;
using Presentation_layer;
using System.Net.Http.Headers;
namespace danish
{
	public class controller
	{
		public static void Main(String[] args)
		{
			Emp BO = new Emp();

			PL pl = new PL();
			BL bl = new BL();
			DAL dal = new DAL();

			BO = pl.getinput(BO);
			BO = bl.calculate(BO);
			if (dal.Save(BO) == true)
			{
				Console.WriteLine("saved");
				Console.WriteLine(BO.Salary);
			}
		}
	}
}